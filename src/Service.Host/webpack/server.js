﻿const path = require('path');
const webpack = require('webpack');

const Server = require('webpack-dev-server');

const config = require('./webpack.config');

const devServer = new Server(
    {
        static: {
            directory: path.join(__dirname, '../')
        },
        client: { overlay: false },
        devMiddleware: {
            index: true,
            mimeTypes: { 'text/html': ['phtml'] },
            serverSideRender: true,
            writeToDisk: true
        },
        host: '127.0.0.1',
        hot: true,
        historyApiFallback: true,
        port: 3000
    },
    webpack(config)
);

(async () => {
    await devServer.start();

    console.log('Running');
})();
