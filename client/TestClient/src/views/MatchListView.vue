<script setup lang="ts">
import matchesStore,{_Matches} from "@/stores/matches.ts";
</script>

<template>
    <div class="overflow-hidden rounded-md bg-white shadow">
        <ul role="list" class="divide-y divide-gray-200">
            <li v-for="match in _Matches" :key="match.matchId" class="px-6 py-4">
                <div class="flex w-full">
                        <span class="w-1/3 border flex items-center justify-center">
                            Event Image Here
                        </span>
                    <div class="w-2/3">
                        <div class="flex w-1/2">
                                <span class="flex border rounded-md px-2">
                                    Team 1 : {{match.team1}}
                                </span>
                        </div>
                    </div>
                </div>
            </li>
        </ul>
    </div>
</template>

<style scoped>

</style>