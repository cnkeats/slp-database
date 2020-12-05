const glob = require("glob");
const { default: SlippiGame } = require('@slippi/slippi-js');
const readlineSync = require('readline-sync');
const fs = require('fs');;
const path = require('path');
const pjson = require('./package.json');
const jsonLock = require('./package-lock.json');
const crypto = require('crypto');
const util = require('util');
const notifier = require('node-notifier');
const FormData = require('form-data');
const _7z = require('7zip-min');
var request = require('request');
const fetch = require('node-fetch');
process.env['NODE_TLS_REJECT_UNAUTHORIZED'] = 0;

function log(obj) {
    console.log(util.inspect(obj, false, null, true));
}

// Characters ordered by ID
const characters = ['Captain Falcon', 'Donkey Kong', 'Fox', 'Mr. Game & Watch', 'Kirby', 'Bowser',
    'Link', 'Luigi', 'Mario', 'Marth', 'Mewtwo', 'Ness', 'Peach', 'Pikachu',
    'Ice Climbers', 'Jigglypuff', 'Samus', 'Yoshi', 'Zelda', 'Sheik', 'Falco',
    'Young Link', 'Dr. Mario', 'Roy', 'Pichu', 'Ganondorf'];

const characters_lowercase = ['captain falcon', 'donkey kong', 'fox', 'mr. game & watch', 'kirby', 'bowser',
    'link', 'luigi', 'mario', 'marth', 'mewtwo', 'ness', 'peach', 'pikachu',
    'ice climbers', 'jigglypuff', 'samus', 'yoshi', 'zelda', 'sheik', 'falco',
    'young link', 'dr. mario', 'roy', 'pichu', 'ganondorf'];

// Stages ordered by ID
const stages = [null, null, 'Fountain of Dreams', 'Pokémon Stadium', "Princess Peach's Castle", 'Kongo Jungle',
    'Brinstar', 'Corneria', "Yoshi's Story", 'Onett', 'Mute City', 'Rainbow Cruise', 'Jungle Japes',
    'Great Bay', 'Hyrule Temple', 'Brinstar Depths', "Yoshi's Island", 'Green Greens', 'Fourside',
    'Mushroom Kingdom I', 'Mushroom Kingdom II', null, 'Venom', 'Poké Floats', 'Big Blue', 'Icicle Mountain',
    'Icetop', 'Flat Zone', 'Dream Land N64', "Yoshi's Island N64", 'Kongo Jungle N64', 'Battlefield', 'Final Destination'];

function loadGameData(file) {

    const fileSource = 'Krohnos';

    let data = { fileSource }
    try {
        const game = new SlippiGame(file);
        data.settings = game.getSettings();
        data.metadata = game.getMetadata();
        data.gameend = game.getGameEnd();
        
        data.stats = game.getStats().overall.map((o) => o.killCount);
        
        data.startingSeed = game.getFrames()[0].players[0].pre.seed
        data.filename = path.basename(file);

        console.log(data.filename);

        const p1Character = `${data.settings.players[0].characterId}_${data.settings.players[0].characterColor}`;
        const p2Character = `${data.settings.players[1].characterId}_${data.settings.players[1].characterColor}`;
        const startingSeed = data.startingSeed;
        const stageId = data.settings.stageId;
        const gameLength = data.metadata.lastFrame;
        const gameMode = data.settings.gameMode;
        const scene = data.settings.scene;

        data.hash = crypto.createHash('md5').update(`${p1Character}_${p2Character}_${startingSeed}_${stageId}_${gameLength}_${gameMode}_${scene}`).digest("hex");
        
        const latestFrame = game.getLatestFrame();        
        if (latestFrame != null) {
            data.latestFramePercents = latestFrame.players.map((p) => p.post.percent);
        }
        return data
    } catch(e) {
        console.log(`Error reading metadata. Ignoring... (${file})`)
        console.log(e);
        return
    }
}

async function submitGame(gameData, targetFilePath, stream) {

    console.log(JSON.stringify(gameData));
    
    var options = {
        'method': 'POST',
        'url': 'https://localhost:44314/Game/SubmitFile',
        'headers': {
        },
        formData: {
            'File': {
                'value': stream,
                'options': {
                    'filename': targetFilePath,
                    'contentType': null
                }
            },
            'Submitter': 'Krohnos',
            'SlpReplay': JSON.stringify(gameData)
        }
    };

    request(options, function (error, response) {
        if (error) {
            return false;
        };

        data = JSON.parse(response.body);

        console.log(`File: ${gameData.filename}`);
        console.log(`Message: ${data.message}`);
        if (data.stackTrace != null) {
            console.log(`Stack Trace: ${data.stackTrace}`);
        }
    });


    return true;
}

async function processFiles(files) {
    console.time('Processing');
    let goodFiles = 0;
    let badFiles = 0;

    files = [files[0]];

    for (const file of files) {
        const gameData = loadGameData(file);

        const dirPath = path.dirname(file);
        const newFilePath = dirPath + "/submitted/" + path.basename(file).slice(0, -4) + ".7z";
        _7z.pack(file, newFilePath, err => {
            
        });

        const stream = fs.createReadStream(newFilePath);

        const success = await submitGame(gameData, newFilePath, stream);

        if (success) {
            goodFiles++;
        }
        else {
            badFiles++;
        }
        if ((goodFiles + badFiles) % 10 == 0) {
            console.log(`${goodFiles + badFiles} of ${files.length} processed`);
            console.timeLog('Processing');
        }
    }

    notify = true
    if (notify && files.length > 20) {
        notifier.notify({
            title: `Finished processing`,
            message: `${goodFiles + badFiles} replays processed.\n${badFiles} failed to submit.`,
            icon: path.join(__dirname, 'slippi_icon.png')
        });
    }
    console.log(`Finished processing`);
    console.log(`${goodFiles + badFiles} replays processed.`);
    if (badFiles > 0) {
        console.log(`${badFiles} failed to submit.`);
    }
    console.timeEnd('Processing');
}

// File path for replays to be processed
const filePath = "C:/Users/Calvin/Documents/prog/slp-database/uploader/data/testing/*.slp"

console.log(`${glob.sync(filePath).length} files in glob`);

processFiles(glob.sync(filePath));

//const f = "C:/Users/Calvin/Documents/prog/slp-database/uploader/data/testing/*.slp";

/*function zipAllFiles(files) {

    for (const file of files) {

        zipFilePath = path.dirname(file) + "/zipped/test/etc/lol/" + path.basename(file).slice(0, -4);

        console.log(zipFilePath);
        _7z.pack(file, zipFilePath, err => {

        });
    }
}

zipAllFiles(glob.sync(filePath));
*/
//_7z.pack(f, "C:/Users/Calvin/Documents/prog/slp-database/uploader/data/testing/Game_20201129T235716.7z", err => {

//});
