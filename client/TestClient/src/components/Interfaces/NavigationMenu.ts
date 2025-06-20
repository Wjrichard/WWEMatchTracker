import type { IconClassName } from "@/components/utilities/Icons.ts";
import type { uiRouteRecord } from "./Router";

interface NavigationItemBase {
  label: string;
  icon?: IconClassName;
}

export interface NavigationSingleRoute extends NavigationItemBase {
  route: uiRouteRecord;
}
export interface NavigationAction extends NavigationItemBase {
  customAction: () => void;
}
export type NavigationItem =
    | NavigationSingleRoute
    | NavigationAction
    | NavigationItemGroup;

export interface NavigationItemGroup {
  label?: string;
  icon?: IconClassName;
  items: Array<NavigationItem>;
}
export type NavigationItemGroups = Array<NavigationItemGroup>;
