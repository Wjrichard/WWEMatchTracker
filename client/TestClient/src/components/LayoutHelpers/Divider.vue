<script setup lang="ts">
import { Separator } from "reka-ui";
import { twMerge } from "tailwind-merge";
import { defaultSeparatorStyles } from "../Styles/DefaultGenerators";

defineProps<{
  orientation?: "horizontal" | "vertical";
}>();
</script>

<template>
  <Separator
    :orientation="orientation"
    :class="[
      twMerge(
        defaultSeparatorStyles(orientation ?? 'horizontal'),
        'data-[orientation=horizontal]:h-px data-[orientation=horizontal]:w-full data-[orientation=vertical]:h-full data-[orientation=vertical]:w-px',
      ),
    ]"
  />
</template>
