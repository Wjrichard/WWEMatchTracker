<script setup lang="ts">
import {useRouter,useRoute} from "vue-router";
import type {Match} from "@/types/match.ts";
import Dialog from "@/components/Layout/Dialog.vue";
import {_Matches, loadMatchDetails, _CurrentMatchTeams, setMatch, selectedMatch} from "@/stores/matches.ts";
import eventStore,{selectedEvent} from "@/stores/events.ts";
import {onBeforeMount, ref} from "vue";
import {PlusCircleIcon} from "@heroicons/vue/24/solid";

const $router = useRouter();
const $route = useRoute()

const newMatches = ref<Match[]>([])

const showAddMatchDialog = ref(false)

onBeforeMount(async () => {
    await loadMatchDetails(eventStore.selectedEvent.value.event.eventId)
})

async function openMatchDetails(matchId:number){
    setMatch(matchId)
    await $router.push({ name: 'matchDetails', params: { matchId: matchId } });
}

// New function for adding an event
async function addNewMatch() {
    newMatches.value.push({
        matchId: 0,
        matchName: ''
    });
}

onBeforeMount(async () => {
    const eventIdParam = $route.params.eventId;
    let eventId: number | undefined = undefined;

    if (typeof eventIdParam === "string") {
        eventId = Number(eventIdParam);
    } else if (Array.isArray(eventIdParam) && eventIdParam.length > 0) {
        eventId = Number(eventIdParam[0]);
    }

    if (eventId && !isNaN(eventId)) {
        await loadMatchDetails(eventId);
    } else {
        console.error("Invalid eventId in route params:", eventIdParam);
    }
});
</script>

<template>
    <div class="relative">
        <div class="overflow-y-auto rounded-md bg-white shadow">
            <div class="sticky top-0 z-10 bg-white">
                <h1 class="w-full text-center flex items-center justify-center py-12 text-8xl font-bold">
                    {{ selectedEvent.event.eventName }}
                </h1>
            </div>
            <div class="overflow-hidden rounded-md bg-white shadow">
                <ul role="list" class="divide-y divide-gray-200">
                    <li v-for="detail in _Matches" class="px-6 py-4 hover:cursor-pointer hover:bg-gray-50" @click="openMatchDetails(detail.match.matchId)">
                        <div class="flex w-full" v-if="detail.match.matchName !== '' && detail.match.matchName">
                            <span class="w-1/3 flex items-center font-bold text-2xl whitespace-pre-wrap">
                                {{detail.match.matchName}}
                            </span>

                            <div class="w-2/3 flex justify-center">
                                <div class="mx-auto max-w-7xl px-6 lg:px-8 w-full flex justify-center">
                                    <div class="-mx-6 gap-1 overflow-hidden sm:mx-0 sm:rounded-2xl flex w-full justify-center">
                                        <div v-for="team in detail.teams" class="bg-gray-200 rounded-lg p-8 hover:bg-green-200 text-center cursor-pointer font-semibold">
                                            {{team.teamName}}
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </li>
                </ul>
                <div class="sticky bottom-0 border-t border-gray-200 bg-white flex w-full justify-center py-6 shadow-md z-20">
                    <div class="hover:bg-gray-100 cursor-pointer flex items-center" @click="addNewMatch">
                        <PlusCircleIcon class="h-15 w-15" />
                    </div>
                </div>
            </div>
        </div>
    </div>
    
</template>