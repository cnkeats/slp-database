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

function tfetch(url, options, timeout = 7000) {
    return Promise.race([
        fetch(url, options),
        new Promise((_, reject) =>
            setTimeout(() => reject(new Error('timeout')), timeout)
        )
    ]);
}

function loadGameData(file) {

    const fileSource = 'Krohnos';

    let data = { fileSource }
    try {
        const game = new SlippiGame(file);
        data.settings = game.getSettings();
        data.metadata = game.getMetadata();
        data.gameend = game.getGameEnd();
        
        data.stats = game.getStats();
        
        data.startingSeed = game.getFrames()[0].players[0].pre.seed
        data.filename = path.basename(file);

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

async function submitGame(gameData) {

    try {
        //return await tfetch('http://slippi.ventechs.net:82/API/SubmitGame', {
        return await fetch('https://localhost:44314/API/SubmitGame', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(gameData)
        }, 20000)
        .then(responsePromise => {
            return responsePromise.json()
                .then(
                    data => {
                        if (!data.success) {
                            console.log(`File: ${gameData.filename}`);
                            console.log(`Message: ${data.message}`);
                            if (data.stackTrace != null) {
                                console.log(`Stack Trace: ${data.stackTrace}`);
                            }
                        }
                        this.data = data;
                        return this;
                    }
                )
        });
    }
    catch (e) {
        console.log(e);
        this.success = false;
        this.exception = e;
        return this;
    }
}

async function submitReplay(file, gameId, uploaderId) {

    const stream = fs.createReadStream(file);

    var formdata = new FormData();
    formdata.append("GameId", gameId);
    formdata.append("UploaderId", uploaderId);
    formdata.append("File", stream, file);
    
    var requestOptions = {
        method: 'POST',
        body: formdata,
        redirect: 'follow'
    };

    //return await tfetch('http://slippi.ventechs.net:82/API/SubmitReplayFile', requestOptions,  20000)
    return await fetch('https://localhost:44314/API/SubmitReplayFile', requestOptions, 20000)
    .then(responsePromise => {
        return responsePromise.json()
            .then(
                data => {
                    if (!data.success) {
                        console.log(`File: ${gameData.filename}`);
                        console.log(`Message: ${data.message}`);
                        if (data.stackTrace != null) {
                            console.log(`Stack Trace: ${data.stackTrace}`);
                        }
                    }
                    this.data = data;
                    return this;
                }
            )
    });
}

async function processFiles(files, uploaderId, uploadFile) {
    console.time('Processing');
    let goodFiles = 0;
    let badFiles = 0;
    let replays = 0;

    for (const file of files) {
        const gameData = loadGameData(file);

        console.log(gameData.metadata.players);
        console.log(gameData.settings);
        return;
        
        let success = false;
        try {
            if (gameData) {
        
                const gameResponse = await submitGame(gameData);
                
                success = false;
                if (gameResponse.data != null && gameResponse.data.success) {
                
                    if (uploadFile) {
                        const replayResponse = await submitReplay(file, gameResponse.data.gameId, uploaderId);
                    
                        if (replayResponse.data.success) {
                            success = true;
                            replays++;
                        }
                        else {
                        }
                    }
                    else {
                        success = true;
                    }
                }
            }
        }
        catch (ex) {
            console.log(`Exception hit. Skipping replay ${file}.`);
            console.log(ex);
        }
        
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
            message: `${goodFiles + badFiles} replays processed.\n${badFiles} failed to submit.\n${replays} replays uploaded.`,
            icon: path.join(__dirname, 'slippi_icon.png')
        });
    }
    console.log(`Finished processing`);
    console.log(`${goodFiles + badFiles} replays processed.`);
    if (badFiles > 0) {
        console.log(`${badFiles} failed to submit.`);
        console.log(`${replays} replays uploaded.`);
    }
    console.timeEnd('Processing');
}

// File path for replays to be processed
//const filePath = "D:/SlippiReplays/testing/temp/badLRAS/testing/**/*.slp"
//const filePath = "D:/SlippiReplays/replayDumps/**/Game_20201216T183112.slp"
const filePath = "D:/SlippiReplays/testing/test3.slp"

// True to upload the .slp files in addition to the metadata
const uploadFile = false;
console.log(`${glob.sync(filePath).length} files in glob`);

processFiles(glob.sync(filePath), 0, uploadFile);