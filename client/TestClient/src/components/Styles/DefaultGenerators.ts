import { twMerge } from "tailwind-merge";
import { tw } from "@/components/utilities/tw.ts";

export type Colors = "success" | "warning" | "error" | "info" | "danger";
export type Variants = "solid" | "outline" | "soft" | "subtle" | "ghost";

// Allows optional color for consistent usage in components even if no color styles are wanted
export function generateColorClasses(
    color?: string | Colors,
    variant?: Variants,
    withFill?: boolean,
) {
    let mainColor =
        color === "success"
            ? "green-600"
            : color === "warning"
                ? "yellow-500"
                : color === "error" || color === "danger"
                    ? "red-600"
                    : color === "info"
                        ? "blue-600"
                        : "";

    if (mainColor === "") return "";

    let baseColors = tw`bg-${mainColor} hover:bg-${mainColor}/75 disabled:opacity-75 text-white ${withFill && "fill-white"}`;

    if (variant === "outline") {
        return twMerge(
            baseColors,
            defaultRingColorClasses(),
            tw`text-${mainColor} bg-transparent hover:bg-${mainColor}/10 ring-${mainColor}/50 ${withFill && `fill-${mainColor}`}`,
        );
    } else if (variant === "soft") {
        return twMerge(
            baseColors,
            tw`text-${mainColor} bg-${mainColor}/10 hover:bg-${mainColor}/15 ${withFill && `fill-${mainColor}`}`,
        );
    } else if (variant === "subtle") {
        return twMerge(
            baseColors,
            defaultRingColorClasses(),
            tw`text-${mainColor} bg-${mainColor}/10 hover:bg-${mainColor}/15 ring-${mainColor}/25 ${withFill && `fill-${mainColor}`}`,
        );
    } else if (variant === "ghost") {
        return twMerge(
            baseColors,
            tw`text-${mainColor} bg-transparent hover:bg-${mainColor}/10 ${withFill && `fill-${mainColor}`}`,
        );
    }

    return baseColors;
}

export function generatePrimarySecondaryColorClasses(
    color?: string | "primary" | "secondary",
    withFill?: boolean,
) {
    if (color === "primary") {
        return tw`bg-zinc-900 hover:bg-zinc-900/75 text-white ${withFill && "fill-white"}`;
    } else if (color === "secondary") {
        return twMerge(
            defaultRingColorClasses(),
            tw`bg-transparent text-zinc-950 hover:bg-zinc-100`,
            withFill && "fill-zinc-950",
        );
    }
    return "";
}

export function defaultRingColorClasses(variant?: "dark") {
    return twMerge(
        tw`ring ring-inset ring-zinc-200`,
        variant === "dark" ? "ring-zinc-400" : "",
    );
}

export function defaultSeparatorStyles(
    orientation?: "horizontal" | "vertical",
) {
    if (orientation === "vertical") {
        return tw`shrink-0 bg-zinc-200 w-[1px] mx-[5px]`;
    }
    return tw`shrink-0 bg-zinc-200 h-[1px] my-[5px]`;
}