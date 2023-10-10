const PROXY_CONFIG = [
    {
        context: [
            '/',
        ],
        target: "https://localhost:7217/",
        secure: false,
        changeOrigin: true, 
        pathRewrite:{
            "^/":""
        }
    }
]

module.exports = PROXY_CONFIG;