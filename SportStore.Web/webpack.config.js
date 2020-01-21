const path = require('path');
const MiniCssExtractPlugin = require('mini-css-extract-plugin');
const CopyPlugin = require('copy-webpack-plugin');

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
    plugins: [
        new MiniCssExtractPlugin({
            filename: "index.css"
        }),
        new CopyPlugin([
            { from: './node_modules/bootstrap/dist', to: 'libs/bootstrap' },
            { from: './node_modules/@fortawesome/fontawesome-free/css', to: 'libs/fortawesome/css' },
        ]),
    ],
};