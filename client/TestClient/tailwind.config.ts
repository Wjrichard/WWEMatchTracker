export default {
    content: [
        "./index.html",
        "./src/**/*.{ts,tsx,vue}",
    ],
};
// tailwind.config.ts
module.exports = {
    // ...existing config...
    safelist: [
        'bg-green-600', 'bg-yellow-500', 'bg-red-600', 'bg-blue-600', 'bg-gray-900',
        'text-green-600', 'text-yellow-500', 'text-red-600', 'text-blue-600', 'text-gray-900',
        'hover:bg-green-600/75', 'hover:bg-yellow-500/75', 'hover:bg-red-600/75', 'hover:bg-blue-600/75', 'hover:bg-gray-900/75',
        'ring-green-600', 'ring-yellow-500', 'ring-red-600', 'ring-blue-600', 'ring-gray-900',
        // ...add any other dynamic classes you use...
    ],
};
