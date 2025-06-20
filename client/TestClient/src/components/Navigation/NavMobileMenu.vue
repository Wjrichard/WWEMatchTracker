<script setup lang="ts">
import {
  DialogRoot,
  DialogTrigger,
  DialogPortal,
  DialogOverlay,
  DialogContent,
  DialogClose,
} from "reka-ui";
import Button from "../General/Button.vue";
import { defaultRingColorClasses } from "../Styles/DefaultGenerators";
import type { NavigationItemGroups } from "../Interfaces/NavigationMenu";
import NavigationMenu from "./NavigationMenu.vue";
import { twMerge } from "tailwind-merge";

export interface MobileMenuProps {
  currentRoutePath: string;
  navigationEntries: NavigationItemGroups;
}

defineProps<MobileMenuProps>();
</script>

<template>
  <div class="flex justify-between">
    <DialogRoot>
      <DialogTrigger data-testid="mobile-sidebar-trigger">
        <Button icon="icon-menu" color="secondary" variant="ghost" />
      </DialogTrigger>
      <DialogPortal>
        <DialogOverlay class="fixed inset-0 bg-black/30" />
        <DialogContent class="fixed top-0 h-full p-4">
          <div
            class="flex h-full w-80 flex-col rounded-lg bg-white p-2 ring-zinc-950/5"
            :class="[defaultRingColorClasses()]"
          >
            <div class="shrink-0">
              <DialogClose>
                <Button icon="icon-x" color="secondary" variant="ghost" />
              </DialogClose>
            </div>
            <div
              v-if="$slots.header"
              :class="
                twMerge(
                  'flex shrink-0 flex-col border-b border-zinc-200 p-2 lg:p-4',
                )
              "
            >
              <slot name="header"></slot>
            </div>
            <div class="grow overflow-y-auto">
              <NavigationMenu
                orientation="vertical"
                :navigation-entries="navigationEntries"
                :current-route-path="currentRoutePath"
              />
            </div>
          </div>
        </DialogContent>
      </DialogPortal>
    </DialogRoot>
    <div class="flex grow justify-end">
      <slot name="footer"></slot>
    </div>
  </div>
</template>
