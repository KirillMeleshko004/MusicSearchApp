const path = require('path');

let mode = 'development';
if (process.env.NODE_ENV === 'production') {
    mode = 'production';
}

module.exports = {
    mode,

    infrastructureLogging:
    {
        stream: process.stdout,
        colors: true,
        level: 'info'
    },

    entry: [
        './src/jsx/index.jsx',
    ],

    output: {
        path: path.resolve(__dirname, '/build'),
        publicPath: "/",
        clean: true,
        filename: "bundle.js"
    },
    devServer: {
        historyApiFallback: true,
        hot: true,
        port: 8081,
        open: false,
    },
    module: {
        rules: [
            {
                //for jsx files
                test: /\.jsx?/,
                exclude: /node_modules/, // не обрабатываем файлы из node_modules
                use: {
                    loader: 'babel-loader',
                    
                },
            },
            {
                //for svg files
                test: /\.svg$/,
                use: {     
                    loader: 'url-loader',
                    loader: 'file-loader',
                }
            },
            {
                //for scss and sass files
                test: /\.s[ac]ss$/,
                use: [
                    "style-loader", //adds css To dom
                    "css-loader", //resolve all devendecies and translate to js
                    "sass-loader", // Compiles Sass to CSS
                ],
            },
        ],
    },
    stats: 'errors-only',
}
