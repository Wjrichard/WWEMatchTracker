<script setup lang="ts">
import Item, { type ItemProps } from "../LayoutHelpers/Item.vue";
import {
  type Colors,
  generateColorClasses,
  generateSecondaryColorClasses,
  type Variants,
} from "@/components/Styles/DefaultGenerators";
import { twMerge } from "tailwind-merge";
import { RouterLink } from "vue-router";
import { tw } from "@/utilities/tw.ts";
import type { uiRouteRecord } from "../Interfaces/Router";
import type { IconClassName } from "../utilities/Icons.ts";

export interface ButtonProps {
  item?: ItemProps;
  label?: string;
  icon?: IconClassName;
  color?: "primary" | "secondary" | Colors;
  variant?: Variants;
  fullWidth?: boolean;
  disabled?: boolean;
  working?: boolean;
  to?: uiRouteRecord;
}

defineProps<ButtonProps>();
</script>

<template>
  <component
    :is="to ? RouterLink : 'button'"
    :to="to"
    class="relative flex justify-center rounded px-2 py-1 select-none"
    :disabled="disabled || working"
    :class="[
      twMerge(
        tw`cursor-pointer rounded-md px-4 py-2 font-medium disabled:cursor-not-allowed disabled:opacity-75`,
        color === 'secondary'
          ? generateSecondaryColorClasses(color, true, true)
          : generateColorClasses(color ?? 'primary', variant, true, true),
        fullWidth ? 'w-full' : '',
      ),
    ]"
    :role="to ? 'link' : 'button'"
  >
    <div class="absolute top-0 h-full w-full" v-if="working">
      <div class="flex h-full items-center justify-center text-blue-500">
        <div class="animate-ping">
          <div class="size-2">
            <svg viewBox="0 0 24 24" xmlns="http://www.w3.org/2000/svg">
              <circle r="11" cx="12" cy="12" />
            </svg>
          </div>
        </div>
      </div>
    </div>

    <Item v-if="item" :class="[working && 'opacity-0']" v-bind="item" />
    <Item
      v-else
      :class="[working && 'opacity-0']"
      :label="label"
      :icon-left="icon"
    />
  </component>
</template>
