'use strict';

const
	webpack = require('webpack'),
	path = require('path');

const src_path = './src/';

module.exports = {

	watch: true,

	entry: {
		home: [
			src_path + 'js/home.js'
		]
	},

	output: {
		path: path.join(__dirname, 'content'),
		publicPath: '/content/',
		filename: 'js2017/[name].js'
	},

	resolve: {
		extensions: ['', '.js', '.jsx']
	},

    externals: {
        $: "jQuery",
        _: "lodash",
		FastClick: "FastClick"
    },

	module: {
		loaders: [
			{
				test: /\.js?$/,
				loader: 'babel',
				exclude: /node_modules/
			}
		]
	},

	plugins: [
    	new webpack.NoErrorsPlugin()
	]

};
