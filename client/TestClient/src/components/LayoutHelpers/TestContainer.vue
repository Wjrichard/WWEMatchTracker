<script setup lang="ts">
import { snakeCase } from "change-case";
import Divider from "./Divider.vue";

defineProps<{
  /** The section header text */
  header: string;
  /** Optional description for the section. Shorthand for the #description slot */
  description?: string;
  /** The data-testid for the container */
  testid: string;
  /** Container height in pixels. Otherwise it'll fit to content */
  containerHeight?: number | string;
}>();
</script>

<template>
  <div class="flex h-full flex-col">
    <div class="flex shrink-0 items-center space-x-2">
      <a
        class="relative text-2xl font-medium before:absolute before:top-0 before:-left-4.5 before:opacity-50 hover:underline hover:before:content-['#']"
        role="heading"
        aria-level="1"
        :href="`#${snakeCase(header)}`"
        :id="snakeCase(header)"
      >
        {{ header }}
      </a>
      <slot name="start" />
      <div class="flex grow items-center justify-end space-x-2">
        <slot name="end" v-bind="{ testid }">
          <div
            class="rounded bg-zinc-200 px-2 font-mono text-sm select-all"
            title="Test ID"
          >
            {{ testid }}
          </div>
        </slot>
      </div>
    </div>
    <Divider class="mb-6" />
    <div class="mb-3 lg:px-4" v-if="$slots.description || description">
      <slot name="description">
        {{ description }}
      </slot>
    </div>
    <div
      class="lg:px-4"
      :class="{
        'overflow-y-auto': containerHeight,
      }"
      :data-testid="testid"
      :style="containerHeight ? `height: ${containerHeight}px` : 'grow'"
    >
      <slot />
    </div>
  </div>
</template>
