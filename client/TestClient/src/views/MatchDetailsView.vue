<script setup lang="ts">
import matchesStore from '@/stores/matches'
import {onMounted} from "vue";

onMounted(async () => {
 
})
</script>

<template>
  <div v-for="participant in matchesStore._CurrentMatchTeams"
        class="flex w-full">
       {{participant}}
  </div>
</template>
