<script setup lang="ts">
import Icon from "../General/Icon.vue";

defineProps<{
  label: string;
  href: string;
  visitedHighlight?: boolean;
}>();
</script>

<template>
  <a
    :href="href"
    target="_blank"
    class="inline-flex items-center gap-2 text-blue-500 hover:text-blue-900"
    :class="[
      visitedHighlight
        ? 'visited:text-purple-600 visited:hover:text-purple-800'
        : '',
    ]"
    ><span>{{ label }}</span
    ><Icon name="icon-external-link"
  /></a>
</template>
