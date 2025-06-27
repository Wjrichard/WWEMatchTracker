<script setup lang="ts">
import type { NavigationItemGroups } from "../Interfaces/NavigationMenu.ts";
import NavigationMenu from "../Navigation/NavigationMenu.vue";
import NavMobileMenu from "../Navigation/NavMobileMenu.vue";
import { twMerge } from "tailwind-merge";

export interface SidebarLayoutProps {
  currentRoutePath: string;
  navigationEntries: NavigationItemGroups;
  allowHorizontalScroll?: boolean;
  /** If true, disables vertical scroll in the main content area */
  disallowVerticalScroll?: boolean;
}

defineProps<SidebarLayoutProps>();
</script>

<template>
  <div
    class="flex h-full w-full flex-col bg-transparent lg:flex-row lg:bg-zinc-100 z-99"
  >
    <div class="w-full lg:w-72">
      <div class="flex h-full flex-col">
        <div
          v-if="$slots.sidebarHeader"
          class="hidden shrink-0 border-b border-zinc-200 p-2 lg:block lg:p-4"
        >
          <slot name="sidebarHeader" />
        </div>
        <NavigationMenu
          class="hidden p-4 lg:flex"
          orientation="vertical"
          :navigationEntries="navigationEntries"
          :currentRoutePath="currentRoutePath"
          style="word-break: break-word; white-space: normal;"
        />
        <div
          v-if="$slots.sidebarFooter"
          class="mt-auto hidden shrink-0 border-t border-zinc-200 p-4 lg:block"
        >
          <slot name="sidebarFooter" />
        </div>
        <NavMobileMenu
          class="flex bg-white px-2 py-2 lg:hidden"
          :navigation-entries="navigationEntries"
          :current-route-path="currentRoutePath"
        >
          <template v-if="$slots.sidebarHeader" #header>
            <slot name="sidebarHeader" />
          </template>
          <template v-if="$slots.mobileFooter" #footer>
            <slot name="mobileFooter" />
          </template>
        </NavMobileMenu>
      </div>
    </div>
    <div
      class="flex flex-1 flex-col bg-zinc-100 lg:min-w-0 lg:pt-2 lg:pr-2 lg:pb-2"
    >
      <main
        :class="
          twMerge(
            'relative grow overflow-x-hidden overflow-y-auto bg-white lg:rounded-lg lg:ring lg:ring-zinc-200 lg:ring-inset',
            allowHorizontalScroll ? 'overflow-x-auto' : '',
            disallowVerticalScroll ? 'overflow-y-hidden' : '',
          )
        "
      >
        <slot />
      </main>
    </div>
  </div>
</template>
