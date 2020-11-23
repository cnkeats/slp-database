const glob = require("glob");
const { default: SlippiGame } = require('@slippi/slippi-js');
const readlineSync = require('readline-sync');
const fs = require('fs');;
const path = require('path');
const pjson = require('./package.json');
const jsonLock = require('./package-lock.json');
const crypto = require('crypto');
const util = require('util');
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


let files = glob.sync("**/data/*.slp");

if (files.length == 0) {
    readlineSync.question("No replays found. Script should be ran in the same folder or a parent folder of the replays.");
    process.exit()
}

//console.log(`${files.length} replays found.`);

//files = [files[0]];

files.forEach((file, i) => {
    const gameData = loadGameData(file, i);
    submitGame(gameData);
})

function loadGameData(file, i) {
    filename = path.basename(file);
    const hash = crypto.createHash('md5').update(filename).digest("hex");
    let data = { hash }
    try {
        const game = new SlippiGame(file);
        data.settings = game.getSettings();
        data.metadata = game.getMetadata();

        data.stats = game.getStats().overall.map((o) => o.killCount);
        data.latestFramePercents = game.getLatestFrame().players.map((p) => p.post.percent);
        return data
    } catch(e) {
        console.log(`${i + 1}: Error reading metadata. Ignoring... (${file})`)
        console.log(e);
        return
    }
}

function submitGame(gameData) {

    console.log(`submitting`);

    try {
        fetch('https://localhost:44314/Request/SubmitGame', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(gameData)
            
        }).then(
            response => response.json()
        )
        .then(data => 
            log(data)
        );
    }
    catch (e) {
        console.log(e);
    }


}