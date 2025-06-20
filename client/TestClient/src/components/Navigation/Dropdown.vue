<script setup lang="ts">
import {
  DropdownMenuContent,
  DropdownMenuItem,
  DropdownMenuPortal,
  DropdownMenuRoot,
  DropdownMenuSeparator,
  DropdownMenuTrigger,
} from "reka-ui";
import { twMerge } from "tailwind-merge";
import { RouterLink } from "vue-router";
import { tw } from "@/utilities/tw.ts";
import {
  defaultRingColorClasses,
  defaultSeparatorStyles,
} from "../Styles/DefaultGenerators.ts";
import type { IconClassName } from "@/components/utilities/Icons.ts";
import type { NavigationItemGroups } from "../Interfaces/NavigationMenu.ts";
import Icon from "../General/Icon.vue";

defineProps<{
  label?: string;
  icon?: IconClassName;
  items: NavigationItemGroups;
  outlined?: boolean;
  openSide?: "top" | "right" | "bottom" | "left";
  hideChevron?: boolean;
  narrow?: boolean;
  testid?: string;
}>();
</script>

<template>
  <DropdownMenuRoot>
    <DropdownMenuTrigger
      ref="trigger"
      :class="
        twMerge(
          tw`group flex min-w-fit cursor-pointer items-center justify-between space-x-2 rounded-lg px-2 py-2 hover:bg-zinc-200 data-[state=open]:bg-zinc-200`,
          outlined && defaultRingColorClasses(),
          !narrow && 'w-full',
        )
      "
      :data-testid="testid"
    >
      <slot>
        <div class="flex items-center space-x-2 text-left font-medium">
          <div
            v-if="icon"
            class="flex h-8 w-8 items-center rounded-xl bg-white"
            :class="[defaultRingColorClasses()]"
          >
            <Icon class="mx-auto" :name="icon" />
          </div>
          <span class="whitespace-nowrap">{{ label }}</span>
        </div>
      </slot>
      <Icon
        v-if="!hideChevron"
        class="shrink-0 opacity-50"
        :name="`icon-chevron-${openSide === 'bottom' ? 'down' : openSide === 'top' ? 'up' : (openSide ?? 'down')}`"
      />
    </DropdownMenuTrigger>

    <DropdownMenuPortal>
      <DropdownMenuContent
        align="start"
        :side="openSide"
        :side-offset="4"
        avoid-collisions
        prioritize-position
        :collision-padding="20"
        :class="
          twMerge(
            defaultRingColorClasses(),
            tw`max-h-72 w-fit min-w-40 overflow-y-auto rounded-xl bg-white px-1 py-1 shadow-lg select-none data-[side=bottom]:min-w-(--reka-dropdown-menu-trigger-width) data-[side=top]:min-w-(--reka-dropdown-menu-trigger-width)`,
          )
        "
        :data-testid="testid && `${testid}-dropdown-content`"
      >
        <template v-for="(group, index) in items">
          <DropdownMenuSeparator
            v-if="index > 0"
            :class="twMerge(defaultSeparatorStyles())"
          />
          <DropdownMenuItem
            v-for="item in group.items"
            :as="'route' in item ? RouterLink : 'button'"
            class="flex w-full items-center space-x-2"
            :to="'route' in item ? item.route : undefined"
            @click="
              () => {
                if ('customAction' in item) item.customAction();
              }
            "
            :value="item.label"
            :class="
              twMerge(
                tw`flex cursor-pointer items-center space-x-2 rounded-lg px-3 py-1 text-lg outline-none hover:bg-blue-500 hover:text-white data-[highlighted]:bg-blue-500 data-[highlighted]:text-white`,
              )
            "
          >
            <Icon v-if="item.icon" :name="item.icon" />
            <span>{{ item.label }}</span>
          </DropdownMenuItem>
        </template>
      </DropdownMenuContent>
    </DropdownMenuPortal>
  </DropdownMenuRoot>
</template>
