const path = require('path');
const MiniCssExtractPlugin = require('mini-css-extract-plugin');

module.exports = {
    entry: "./wwwroot/src/index.scss",
    output: {
        path: path.resolve("./wwwroot/dist")
    },
    module: {
        rules: [
            {
                test: /\.s[ac]ss$/i,
                use: [
                    {
                        loader: MiniCssExtractPlugin.loader,
                    },
                    'css-loader',
                    'sass-loader',
                ],
            }
        ],
    },
    plugins: [new MiniCssExtractPlugin( {
        filename: "index.css"
    })],
};