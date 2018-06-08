'use strict';

const
	webpack = require('webpack'),
	path = require('path');

const src_path = './src/';

module.exports = {

	watch: true,

	entry: {
		'FormExample': [
            src_path + 'js/FormExample.js'
		]
	},

	output: {
        path: path.join(__dirname, 'static'),
        publicPath: '/static/',
		filename: 'js/[name].js'
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
    	new webpack.NoErrorsPlugin(),
		new webpack.DefinePlugin({
			'process.env': {
				NODE_ENV: '"production"'
			}
		}),
		new webpack.optimize.UglifyJsPlugin({
			compress: {
				warnings: false
			}
		}),
		new webpack.optimize.DedupePlugin()
	]

};
//
// module.exports.plugins = [
// 	new webpack.DefinePlugin({
// 		'process.env': {
// 			NODE_ENV: '"production"'
// 		}
// 	}),
// 	new webpack.optimize.UglifyJsPlugin({
// 		compress: {
// 			warnings: false
// 		}
// 	}),
// 	new webpack.optimize.OccurenceOrderPlugin()
// ];
