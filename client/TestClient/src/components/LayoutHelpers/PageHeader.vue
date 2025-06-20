<script setup lang="ts">
import Divider from "./Divider.vue";
import { snakeCase } from "change-case";

defineProps<{
  header: string;
  headerAsAnchor?: boolean;
}>();
</script>

<template>
  <div class="flex h-full flex-col">
    <div class="flex shrink-0 items-center space-x-2">
      <component
        :is="headerAsAnchor ? 'a' : 'div'"
        class="text-2xl font-medium"
        role="heading"
        aria-level="1"
        :href="headerAsAnchor && `#${snakeCase(header)}`"
        :id="snakeCase(header)"
      >
        {{ header }}
      </component>
      <slot name="start" />
      <div
        v-if="$slots.end"
        class="flex grow items-center justify-end space-x-2"
      >
        <slot name="end" />
      </div>
    </div>
    <Divider class="mb-6" />
    <div class="grow overflow-y-auto lg:px-4">
      <slot />
    </div>
  </div>
</template>
