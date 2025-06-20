import { twMerge } from "tailwind-merge";
import { tw } from "../../utilities/tw.ts";

export type Colors = "success" | "warning" | "error" | "info" | "danger";
export type Variants = "solid" | "outline" | "soft" | "subtle" | "ghost";

/**
 * Active ring for focusable interactive elements such as inputs
 * @returns {string} - The default active ring color classes
 */
export function defaultActiveRingColorClasses(): string {
  return twMerge(
      tw`focus-within:ring-2 focus-within:ring-blue-500 focus-within:ring-inset`,
  );
}

/**
 * Default ring color classes for elements with borders
 * @returns {string}
 */
export function defaultRingColorClasses(): string {
  return twMerge(tw`ring ring-inset ring-zinc-200`);
}

/**
 * Default styles for a separator element
 *
 * @param orientation - The orientation of the separator (horizontal or vertical)
 * @returns {string} - The default separator styles
 */
export function defaultSeparatorStyles(
    orientation?: "horizontal" | "vertical",
): string {
  if (orientation === "vertical") {
    return tw`shrink-0 bg-zinc-200 w-px mx-[5px]`;
  }
  return tw`shrink-0 bg-zinc-200 h-px my-[5px]`;
}

/**
 * Consistent color classes for elements
 *
 * @param color - The primary coloring of the element (success, warning, error, info, danger)
 * @param variant - The variant of the element (solid, outline, soft, subtle, ghost)
 * @param withFill - Whether to add fill color for SVG icons
 * @returns {string} - Generated Tailwind color classes
 */
export function generateColorClasses(
    color?: string | Colors,
    variant?: Variants,
    withFill?: boolean,
    withHover?: boolean,
    withRing?: boolean,
): string {
  const mainColor =
      color === "success"
          ? "green-600"
          : color === "warning"
              ? "yellow-500"
              : color === "error" || color === "danger"
                  ? "red-600"
                  : color === "info"
                      ? "blue-600"
                      : "gray-900";
  const hoverBg = withHover ? `hover:bg-${mainColor}/75` : "";
  const baseColors = tw`bg-${mainColor} ${hoverBg} disabled:opacity-75 text-white ${withFill ? "fill-white" : ""}`;
  const ringClass = withRing
      ? twMerge(
          defaultRingColorClasses(),
          `focus-within:ring-${mainColor} ring-${mainColor}/75`,
      )
      : "";

  if (variant === "outline") {
    return twMerge(
        baseColors,
        defaultRingColorClasses(),
        ringClass,
        tw`text-${mainColor} bg-transparent ${withHover ? `hover:bg-${mainColor}/10` : ""} ring-${mainColor}/50 ${withFill ? `fill-${mainColor}` : ""}`,
    );
  } else if (variant === "soft") {
    return twMerge(
        baseColors,
        ringClass,
        tw`text-${mainColor} bg-${mainColor}/10 ${withHover ? `hover:bg-${mainColor}/15` : ""} ${withFill ? `fill-${mainColor}` : ""}`,
    );
  } else if (variant === "subtle") {
    return twMerge(
        baseColors,
        defaultRingColorClasses(),
        ringClass,
        tw`text-${mainColor} bg-${mainColor}/10 ${withHover ? `hover:bg-${mainColor}/15` : ""} ring-${mainColor}/25 ${withFill ? `fill-${mainColor}` : ""}`,
    );
  } else if (variant === "ghost") {
    return twMerge(
        baseColors,
        ringClass,
        tw`text-${mainColor} bg-transparent ${withHover ? `hover:bg-${mainColor}/10` : ""} ${withFill ? `fill-${mainColor}` : ""}`,
    );
  }

  return twMerge(baseColors, ringClass);
}

/**
 * Secondary color classes for elements. Only accepts "secondary"
 *
 * @param color
 * @param withFill
 * @param withHover
 * @returns {string}
 */
export function generateSecondaryColorClasses(
    color?: string | "secondary",
    withFill?: boolean,
    withHover?: boolean,
): string {
  if (color === "secondary") {
    return twMerge(
        defaultRingColorClasses(),
        tw`bg-transparent text-zinc-950`,
        withHover && "hover:bg-zinc-100",
        withFill && "fill-zinc-950",
    );
  }
  return "";
}
