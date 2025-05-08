/**
 * Hints to the Tailwind Language Server that the string should be tailwind classes
 */
export const tw = (strings: TemplateStringsArray, ...params: unknown[]) => {
    let str = "";
    for (let i = 0; i < strings.length; i++) {
        str += strings[i] + (params[i] !== undefined ? params[i] : "");
    }
    return str;
};