var http = require('http');
var signalR = require('./signalr.js');

console.log("*************************");
console.log("BuzzCat JS Sample Client");
console.log("*************************");

var serviceUrl = "http://localhost:8177/buzz";

console.log("Service Url: " + serviceUrl);
var client = new signalR.client(serviceUrl, ['BuzzHub'], 2, true);

client.serviceHandlers = { //Yep, I even added the merge syntax here.
    bound: function () { console.log("Websocket bound"); },
    connectFailed: function (error) { console.log("Websocket connectFailed: ", error); },
    connected: function (connection) { console.log("Websocket connected"); },
    disconnected: function () { console.log("Websocket disconnected"); },
    onerror: function (error) { console.log("Websocket onerror: ", error); },
    messageReceived: function (message) { console.log("Websocket messageReceived: ", message); return false; },
    bindingError: function (error) { console.log("Websocket bindingError: ", error); },
    connectionLost: function (error) { console.log("Connection Lost: ", error); },
    reconnecting: function (retry /* { inital: true/false, count: 0} */) {
        console.log("Websocket Retrying: ", retry);
        //return retry.count >= 3; /* cancel retry true */
        return true;
    }
};

client.serviceHandlers.onUnauthorized = function (res) {
    console.log("Websocket onUnauthorized:");
    //TODO: Need to add the authtoken here
    //client.headers.authorization = "token";   
}

client.handlers.buzzhub = {
    error: function (message) {
        console.log("revc => : " + message);
    }
};

setInterval(function () {
    console.log(client.state);
    client.invoke('buzzhub', 'Connect', { Command: "Connect" }).then(function(x){});
}, 2000);

//Manually Start Client
client.start();

