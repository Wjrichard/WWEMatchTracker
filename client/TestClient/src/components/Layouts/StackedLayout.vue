<script setup lang="ts">
import Divider from "../LayoutHelpers/Divider.vue";
import type { NavigationItemGroups } from "../Interfaces/NavigationMenu";
import NavMobileMenu from "../Navigation/NavMobileMenu.vue";
import NavigationMenu from "../Navigation/NavigationMenu.vue";

export type StackedLayoutProps = {
  currentRoutePath: string;
  navigationEntries: NavigationItemGroups;
};

defineProps<StackedLayoutProps>();
</script>

<template>
  <div class="flex h-full w-full flex-col bg-zinc-100">
    <nav class="hidden h-14 justify-between px-2 pt-2 lg:flex">
      <template v-if="$slots.navbarLeft">
        <div class="flex items-center">
          <slot name="navbarLeft" />
        </div>
        <Divider orientation="vertical" />
      </template>
      <NavigationMenu
        class="hidden lg:flex"
        :navigationEntries="navigationEntries"
        :currentRoutePath="currentRoutePath"
      />
      <div v-if="$slots.navbarRight" class="flex items-center">
        <Divider orientation="vertical" />
        <slot name="navbarRight" />
      </div>
    </nav>
    <NavMobileMenu
      class="flex bg-white px-2 py-2 lg:hidden"
      :navigation-entries="navigationEntries"
      :current-route-path="currentRoutePath"
    >
      <template v-if="$slots.navbarLeft" #header>
        <slot name="navbarLeft" />
      </template>
      <template v-if="$slots.mobileFooter" #footer>
        <slot name="mobileFooter" />
      </template>
    </NavMobileMenu>
    <main
      class="relative grow overflow-y-auto bg-white p-6 pt-2 lg:m-2 lg:rounded-lg lg:p-10 lg:ring lg:ring-zinc-200 lg:ring-inset"
    >
      <slot />
    </main>
  </div>
</template>
