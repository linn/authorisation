const config = window.APPLICATION_SETTINGS;
const defaultConfig = { appRoot: 'localhost:51799' };

export default { ...defaultConfig, ...config };
