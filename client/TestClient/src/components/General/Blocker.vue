<script setup lang="ts">
export type BlockerProps = {
  blocked?: boolean;
  working?: boolean;
  text?: string;
};

defineProps<BlockerProps>();
</script>

<template>
  <div class="relative" :class="{ 'pointer-events-none': blocked || working }">
    <div
      v-if="working || blocked"
      class="absolute z-1 flex h-full w-full flex-col items-center justify-center gap-4 bg-zinc-950/70 text-white"
    >
      <div v-if="working" class="animate-ping">
        <div class="size-3 fill-white">
          <svg viewBox="0 0 24 24" xmlns="http://www.w3.org/2000/svg">
            <circle r="11" cx="12" cy="12" />
          </svg>
        </div>
      </div>
      <div v-if="text">{{ text }}</div>
      <slot name="blockerContent" />
    </div>
    <slot />
  </div>
</template>
