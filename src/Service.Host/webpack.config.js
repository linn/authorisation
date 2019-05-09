const path = require('path');
const webpack = require('webpack');

module.exports = {
    entry: {
        app: [
            'babel-polyfill',
            'react-hot-loader/patch', // activate HMR for React
            'webpack-dev-server/client?http://localhost:3000', // bundle the client for webpack-dev-server and connect to the provided endpoint
            'webpack/hot/only-dev-server', // bundle the client for hot reloading (only- means to only hot reload for successful updates)
            './client/src/index.js' // the entry point of our app
        ],
        'silent-renew': './client/silent-renew/index.js'
    },
    output: {
        path: path.join(__dirname, 'client/build'),
        filename: '[name].js',
        publicPath: '/authorisation/build/'
    },
    module: {
        rules: [
            {
                exclude: [
                    /\.html$/,
                    /\.(js|jsx)$/,
                    /\.css$/,
                    /\.scss$/,
                    /\.json$/,
                    /\.svg$/
                ],
                use: {
                    loader: 'url-loader',
                    query: {
                        limit: 10000,
                        name: 'media/[name].[hash:8].[ext]'
                    }
                }
            },
            {
                test: /\.js$/,
                exclude: /(node_modules)/,
                use: {
                    loader: 'babel-loader'
                }
            },
            {
                test: /\.css$/,
                use: [
                    'style-loader',
                    {
                        loader: 'css-loader',
                        options: {
                            importLoaders: 1
                        }
                    },
                    'postcss-loader'
                ]
            },
            {
                test: /\.scss$/,
                use: [
                    'style-loader',
                    {
                        loader: 'css-loader',
                        options: {
                            importLoaders: 1
                        }
                    },
                    'sass-loader',
                    'postcss-loader'
                ]
            },
            {
                test: /\.svg$/,
                use: {
                    loader: 'file-loader',
                    query: {
                        name: 'media/[name].[hash:8].[ext]'
                    }
                }
            }
        ]
    },
    devtool: 'source-map',
    // From https://github.com/gaearon/react-hot-boilerplate/blob/next/webpack.config.js
    plugins: [
        new webpack.HotModuleReplacementPlugin(), // enable HMR globally
        new webpack.NamedModulesPlugin(), // prints more readable module names in the browser console on HMR updates
        new webpack.NoEmitOnErrorsPlugin(), // do not emit compiled assets that include errors
        new webpack.DefinePlugin({
            'PROCESS.ENV': {
                'appRoot': JSON.stringify('http://localhost:61798')
            }
        })
    ]};