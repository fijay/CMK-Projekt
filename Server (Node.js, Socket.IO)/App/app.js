var app = require('http').createServer(handler);
var io = require('socket.io')(app);
var fs = require('fs');

app.listen(80);

function handler(req, res) {
	fs.readFile(__dirname + '/index.html',
		function(err, data) {
			if (err) {
				res.writeHead(500);
				return res.end('Error loading index.html');
			}

			res.writeHead(200, { 'Content-Type': 'text/html' });
			res.end(data);
		});
}

var object = {
	sound: 'a',
	rotation: 1,
	size: 1
};

function getDateAndTime() {
	var date = new Date();

	var hour = date.getHours();
	hour = (hour < 10 ? '0' : '') + hour;

	var min = date.getMinutes();
	min = (min < 10 ? '0' : '') + min;

	var sec = date.getSeconds();
	sec = (sec < 10 ? '0' : '') + sec;

	var year = date.getFullYear();

	var month = date.getMonth() + 1;
	month = (month < 10 ? '0' : '') + month;

	var day = date.getDate();
	day = (day < 10 ? '0' : '') + day;

	return {
		date: year + '-' + month + '-' + day,
		time: hour + ':' + min + ':' + sec,
		dateTime: year + '-' + month + '-' + day + ' ' + hour + ':' + min + ':' + sec
	}
}

function assembleUpdate() {
		var dateAndTime = getDateAndTime();

		return {
			date: dateAndTime.date,
			time: dateAndTime.time,
			dateTime: dateAndTime.dateTime,
			sound: object.sound,
			rotation: object.rotation,
			size: object.size
		};
}

function broadcastUpdate() {
	var update = assembleUpdate();

	console.log('broadcastUpdate', update);
	io.emit('update', update);
}

io.on('connection', function(socket) {
	console.log('socket id: ' + socket.id);
	//console.log(socket.client.conn.remoteAddress + ' connected');
	console.log(socket.handshake.headers['x-forwarded-for'] + ' connected'); // if Node.js is behind proxy

	var update = assembleUpdate();

	console.log('update', update);
	io.to(socket.id).emit('update', update);

	socket.on('disconnect', function() {
		console.log(socket.client.conn.remoteAddress + ' disconnected');
	});

	socket.on('message', function(data) {
		console.log('connection: message');

		console.log('data', data);

		var parsedData = JSON.parse(data);

		console.log('parsedData', parsedData);
 
		if (typeof parsedData.sound !== 'undefined' && parsedData.sound !== null && (parsedData.sound === 'a' || parsedData.sound === 'b' || parsedData.sound === 'c') && parsedData.sound != object.sound) {
			console.log('RECEIVED sound', parsedData.sound);
			object.sound = parsedData.sound;

			broadcastUpdate();
		}

		if (typeof parsedData.rotation !== 'undefined' && parsedData.rotation !== null  && parsedData.rotation >= -10 && parsedData.rotation <= 10 && parsedData.rotation != object.rotation) {
			console.log('RECEIVED rotation', parsedData.rotation);
			object.rotation = +parsedData.rotation;

			broadcastUpdate();
		}

		if (typeof parsedData.size !== 'undefined' && parsedData.size !== null  && parsedData.size >= -10 && parsedData.size <= 10 && parsedData.size != object.size) {
			console.log('RECEIVED size', parsedData.size);
			object.size = +parsedData.size;

			broadcastUpdate();
		}
	});
});
