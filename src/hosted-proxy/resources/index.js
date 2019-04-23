var express = require('express');
var https = require('https');
var app = express();

const podcastRoot = 'https://anchor.fm/stoicmeditations';
const podcastEpisodeRoot = `${podcastRoot}/episodes`;

app.get('/', function(req, res) {
    console.log('getting info for all podcasts');
    getContent(podcastRoot, res);
});

app.get('/marco', function(req, res) {
    console.log('polo');
    res.send('polo');
});
app.get('/health', function(req, res) {
    res.sendStatus(200);
});

app.get('/:podcastId', function(req, res) {
    let podcastId = req.params.podcastId;
    console.log(`podcastId = ${podcastId}`);
    let url = `${podcastEpisodeRoot}/${podcastId}`;
    getContent(url, res);
});

function getContent(url, res) {
    console.log(`Getting content from URL '${url}'`);
    let data = '';
    let dataEvents = 0;
    https.get(url, (resp) => {
        
        // A chunk of data has been recieved.
        resp.on('data', (chunk) => {
            data += chunk;
            dataEvents++;
        });

        // The whole response has been received. Print out the result.
        resp.on('end', () => {
            console.log(`Received ${data.length} characters in ${dataEvents} chunks`);
            let json = extractPodcastJson(data);
            res.setHeader('content-type', 'application/json');
            res.send(json);        
        });        
    });
    return data;
}

function extractPodcastJson(content) {
    let exp = /window.__STATE__ = (?<justData>.*)\;/;

    let match = content.match(exp);
    if (!match) {
        let message = 'extractPodcastJson-no-match-finding-podcast-data';
        console.log(message);
        return message;
    }

    console.log(`justData length = ${match.groups.justData.length}`);
    return match.groups.justData;
}

/*
var fs = require('fs');
app.get('/file', function(req, res) {
    fs.readFile('output.txt', 'utf8', function(err, contents) {
        var json = extractPodcastJson(contents);
        res.setHeader('content-type', 'application/json');
        res.send(json);
    });
});
*/

var server = app.listen(process.env.PORT || 9000, function () {
    console.log(`Listening on port ${server.address().port}`);
});
