<script setup lang="ts">
import {
  NavigationMenuItem,
  NavigationMenuLink,
  NavigationMenuList,
  NavigationMenuRoot,
} from "reka-ui";
import { RouterLink } from "vue-router";
import { twMerge } from "tailwind-merge";
import { tw } from "../../utilities/tw.ts";
import type { NavigationItemGroups } from "../Interfaces/NavigationMenu.ts";
import Dropdown from "./Dropdown.vue";
import { computed } from "vue";
import Icon from "../General/Icon.vue";

export interface SidebarProps {
  currentRoutePath: string;
  navigationEntries: NavigationItemGroups;
  orientation?: "horizontal" | "vertical";
}
const { orientation = "horizontal" } = defineProps<SidebarProps>();

const orientationFlex = computed(() => {
  return `flex ${orientation === "vertical" ? "flex-col" : "flex-row"}`;
});
</script>

<template>
  <NavigationMenuRoot
    :default-value="currentRoutePath"
    :orientation="orientation"
    :class="
      twMerge(
        'relative h-full grow overflow-auto',
        orientation === 'vertical' && '*:w-full',
      )
    "
  >
    <NavigationMenuList :class="twMerge(orientationFlex, tw`font-medium`)">
      <div
        v-for="(group, groupIndex) in navigationEntries"
        :key="groupIndex"
        :class="twMerge(orientationFlex, tw`gap-0.5`)"
      >
        <h3
          v-if="orientation === 'vertical' && group.label"
          :class="
            twMerge(
              tw`mb-1 px-6 text-sm/6 font-medium text-zinc-500`,
              groupIndex > 0 && 'mt-6',
            )
          "
        >
          {{ group.label }}
        </h3>
        <template v-for="entry in group.items">
          <NavigationMenuItem v-if="'items' in entry">
            <Dropdown
              :open-side="orientation === 'horizontal' ? 'bottom' : 'right'"
              :items="[
                {
                  items: entry.items,
                },
              ]"
            >
              <div
                :class="
                  twMerge(
                    'flex items-center gap-2 py-2 text-left font-medium',
                    orientation === 'vertical' ? 'px-4' : '',
                  )
                "
              >
                <Icon v-if="entry.icon" :name="entry.icon" />
                <span
                  :class="[orientation === 'horizontal' && 'whitespace-nowrap']"
                  >{{ entry.label }}</span
                >
              </div>
            </Dropdown>
          </NavigationMenuItem>
          <div v-if="'route' in entry" class="flex justify-between py-0.5">
            <NavigationMenuItem
              :class="
                twMerge(
                  tw`relative flex w-full cursor-pointer items-center gap-3 rounded-lg text-left text-base font-medium hover:bg-zinc-200`,
                  orientation === 'horizontal' ? 'px-3' : 'px-6',
                )
              "
            >
              <NavigationMenuLink
                :as="RouterLink"
                :to="entry.route"
                :class="twMerge(tw`flex w-full items-center gap-2 py-2`)"
              >
                <Icon v-if="entry.icon" :name="entry.icon" />
                <span :class="[orientation && 'whitespace-nowrap']">{{
                  entry.label
                }}</span>
              </NavigationMenuLink>
            </NavigationMenuItem>
          </div>
          <!-- TODO: Support custom action -->
        </template>
      </div>
    </NavigationMenuList>
  </NavigationMenuRoot>
</template>
