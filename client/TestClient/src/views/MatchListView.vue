<script setup lang="ts">
import {useRouter,useRoute} from "vue-router";
import type {MatchDetails} from "@/types/matchDetails.ts";
import {_Matches,loadMatchDetails,_CurrentMatchTeams,setMatch} from "@/stores/matches.ts";
import eventStore,{selectedEvent} from "@/stores/events.ts";
import {onBeforeMount} from "vue";

const $router = useRouter();
const $route = useRoute()

onBeforeMount(async () => {
    await loadMatchDetails(eventStore.selectedEvent.value.event.eventId)
})

async function openMatchDetails(matchId:number){
    setMatch(matchId)
    await $router.push({ name: 'matchDetails', params: { eventId: eventId } });
}
</script>

<template>
    <h1 class="w-full border text-center flex items-center justify-center py-12 text-8xl font-bold">
        {{ selectedEvent.event.eventName }}
    </h1>
    <div class="overflow-hidden rounded-md bg-white shadow">
        <ul role="list" class="divide-y divide-gray-200">
            <li v-for="detail in _Matches" class="px-6 py-4 hover:cursor-pointer hover:bg-gray-50" @click="">
                <div class="flex w-full">
                    <span class="w-1/3 flex items-center font-bold text-2xl whitespace-pre-wrap">
                        {{detail.match.matchName}}
                    </span>
                        
                    <div class="w-2/3 flex">
                        <div >
                            <div class="mx-auto max-w-7xl px-6 lg:px-8">
                                <div class="-mx-6 grid grid-cols-2 gap-0.5 overflow-hidden sm:mx-0 sm:rounded-2xl md:grid-cols-3">
                                    <div v-for="team in detail.teams" class="bg-gray-200 rounded-lg p-8 hover:bg-green-200 cursor-pointer font-semibold">
                                        {{team.teamName}}
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </li>
        </ul>
    </div>
</template>

<style scoped>

</style>