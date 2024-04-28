/** @type {import('next').NextConfig} */
const nextConfig = {
    images: {
        remotePatterns: [
            {
                protocol: 'https',
                hostname: 'w.wallhaven.cc'
            },
            {
                protocol: 'https',
                hostname: 'localhost'
            },
        ],
    },
}

module.exports = nextConfig;