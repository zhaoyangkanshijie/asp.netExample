const
    srcPath = 'src',
    srcScssPath = `${srcPath}/sass`,
    srcLibPath = `${srcPath}/lib`,
    distPath = 'static';

const config = {
    all: {
        html: `views/**`,
        scss: `${srcScssPath}/**`,
        lib: `${srcLibPath}/**`,
    },
    srcPath: {
        scss: {
            'FormExample': [
                'src/sass/FormExample.scss'
            ]
        }
    },
    outPath: {
        path: `${distPath}/**`,
        css: `${distPath}/css`,
        lib: `script/lib`
    }
};

module.exports = config;
