const path = require('path');
const MiniCssExtractPlugin = require('mini-css-extract-plugin');
const CopyPlugin = require('copy-webpack-plugin');
const { CleanWebpackPlugin } = require('clean-webpack-plugin');

module.exports = {
    entry: "./wwwroot/src/index.scss",
    output: {
        path: path.resolve("./wwwroot/dist")
    },
    mode: "development",
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
            { from: './node_modules/@fortawesome/fontawesome-free', to: 'libs/fortawesome' },
            { from: './node_modules/jquery/dist', to: 'libs/jquery' },
        ]),
        new CleanWebpackPlugin(),
    ],
};