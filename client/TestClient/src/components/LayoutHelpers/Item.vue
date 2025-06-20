<script setup lang="ts">
import type { IconClassName } from "@/components/utilities/Icons.ts";

export interface ItemProps {
  iconLeft?: IconClassName;
  iconRight?: IconClassName;
  label?: string;
  labelSecondary?: string;
  labelLayout?: "horizontal" | "vertical";
  labelTextAlignment?: "start" | "center" | "end";
}

defineProps<ItemProps>();
</script>

<template>
  <span class="flex items-center space-x-2">
    <i v-if="iconLeft" :class="[iconLeft]" />
    <span
      v-if="label || labelSecondary"
      class="flex grow"
      :class="[
        labelLayout === 'vertical' ? 'flex-col' : 'flex-row space-x-2',
        labelTextAlignment === 'center'
          ? 'text-center'
          : labelTextAlignment === 'end'
            ? 'text-end'
            : 'text-start',
      ]"
    >
      <span class="" :class="[labelSecondary && 'font-medium']">{{
        label
      }}</span>
      <span class="text-sm" v-if="labelSecondary">{{ labelSecondary }}</span>
    </span>
    <i v-if="iconRight" :class="[iconRight]" />
  </span>
</template>
