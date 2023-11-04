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
        colors: false,
        level: "info"
    },


    entry: [
        './src/jsx/index.jsx',
    ],

    output: {
        path: path.resolve(__dirname, 'build'),
        clean: true,
        filename: "bundle.js"
    },
    devServer: {
        hot: true,
        port: 8081,
        open: true
    },
    
    module: {
        rules: [
            {
                //for jsx files
                test: /\.jsx/,
                exclude: /node_modules/, // не обрабатываем файлы из node_modules
                use: {
                    loader: 'babel-loader',
                    options: {
                        cacheDirectory: true, // Использование кэша для избежания рекомпиляции
                        // при каждом запуске
                    },
                },
            },
            {
                //for svg files
                test: /\.svg$/,
                use: "file-loader",
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
    }
}
