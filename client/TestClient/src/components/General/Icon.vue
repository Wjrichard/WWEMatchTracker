<script setup lang="ts">
import type { IconClassName } from "@/components/utilities/Icons.ts";

export type IconProps = {
  name: IconClassName;
};

const IconProps = defineProps<IconProps>();
</script>

<template>
  <span class="inline-flex w-fit items-center justify-center">
    <i :class="[name]" />
  </span>
</template>
