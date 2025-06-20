<script setup lang="ts">
export type ResponsiveGridProps = {
  smCols?: number | string;
  mdCols?: number | string;
  lgCols?: number | string;
  containerResponsive?: boolean;
};

defineProps<ResponsiveGridProps>();
</script>

<template>
  <div
    :class="{
      '@container': containerResponsive,
    }"
  >
    <div
      :class="[
        `grid grid-cols-1 gap-2 lg:gap-4`,
        `${containerResponsive ? '@' : ''}sm:grid-cols-${smCols ?? 2}`,
        `${containerResponsive ? '@' : ''}md:grid-cols-${mdCols ?? smCols ?? 3}`,
        `${containerResponsive ? '@' : ''}xl:grid-cols-${lgCols ?? smCols ?? lgCols ?? 4}`,
      ]"
    >
      <slot />
    </div>
  </div>
</template>
