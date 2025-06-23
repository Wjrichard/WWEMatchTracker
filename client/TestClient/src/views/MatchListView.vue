<script setup lang="ts">
import {useRouter,useRoute} from "vue-router";
import type {Match} from "@/types/match.ts";
import Dialog from "@/components/Dialogs/Dialog.vue";
import {_Matches, loadMatchDetails, _CurrentMatchTeams, setMatch, selectedMatch} from "@/stores/matches.ts";
import eventStore,{selectedEvent} from "@/stores/events.ts";
import { watch, onBeforeMount, ref, nextTick } from "vue";
import {PlusCircleIcon, PencilIcon} from "@heroicons/vue/24/solid";
import Button from "@/components/General/Button.vue";
import TextInput from "@/components/Forms/TextInput.vue";
import Datepicker from '@vuepic/vue-datepicker';
import '@vuepic/vue-datepicker/dist/main.css';
import matchesStore, { createMatch } from '@/stores/matches';

const $router = useRouter();
const $route = useRoute()

const newMatches = ref<Match[]>([])

// Load newMatches from localStorage if present
const savedNewMatches = localStorage.getItem('newMatches');
if (savedNewMatches) {
  try {
    newMatches.value = JSON.parse(savedNewMatches);
  } catch {}
}

watch(newMatches, (val) => {
  localStorage.setItem('newMatches', JSON.stringify(val));
}, { deep: true });

const isEditing = ref(false)
const dateRange = ref<[Date | null, Date | null]>([null, null]);

function parseDateString(dateString: string): [Date | null, Date | null] {
  if (!dateString) return [null, null];
  // MM/DD/YYYY - MM/DD/YYYY
  const rangeMatch = dateString.match(/(\d{2}\/\d{2}\/\d{4})\s*-\s*(\d{2}\/\d{2}\/\d{4})/);
  if (rangeMatch) {
    return [new Date(rangeMatch[1]), new Date(rangeMatch[2])];
  }
  // YYYY-MM-DD HH:mm:ss
  const dateTimeMatch = dateString.match(/(\d{4}-\d{2}-\d{2})/);
  if (dateTimeMatch) {
    return [new Date(dateTimeMatch[1]), null];
  }
  // MM/DD/YYYY
  const singleDateMatch = dateString.match(/(\d{2}\/\d{2}\/\d{4})/);
  if (singleDateMatch) {
    return [new Date(singleDateMatch[1]), null];
  }
  // YYYY-MM-DD to YYYY-MM-DD
  const isoRangeMatch = dateString.match(/(\d{4}-\d{2}-\d{2})\s*to\s*(\d{4}-\d{2}-\d{2})/);
  if (isoRangeMatch) {
    return [new Date(isoRangeMatch[1]), new Date(isoRangeMatch[2])];
  }
  // YYYY-MM-DD
  const isoSingleMatch = dateString.match(/(\d{4}-\d{2}-\d{2})/);
  if (isoSingleMatch) {
    return [new Date(isoSingleMatch[1]), null];
  }
  return [null, null];
}

watch(
  () => selectedEvent.value?.event?.eventDate,
  (newVal) => {
    if (typeof newVal === 'string') {
      dateRange.value = parseDateString(newVal);
    }
  },
  { immediate: true }
);

watch(dateRange, (newVal) => {
  if (newVal[0] && newVal[1]) {
    const start = newVal[0].toISOString().split('T')[0];
    const end = newVal[1].toISOString().split('T')[0];
    selectedEvent.value.event.eventDate = `${start} to ${end}`;
  } else if (newVal[0]) {
    const start = newVal[0].toISOString().split('T')[0];
    selectedEvent.value.event.eventDate = start;
  }
});

onBeforeMount(async () => {
    const eventIdParam = $route.params.eventId;
    let eventId: number | undefined = undefined;

    if (typeof eventIdParam === "string") {
        eventId = Number(eventIdParam);
    } else if (Array.isArray(eventIdParam) && eventIdParam.length > 0) {
        eventId = Number(eventIdParam[0]);
    }

    if (eventId && !isNaN(eventId)) {
        eventStore.setEvent(eventId); // Ensure selectedEvent is set
        await loadMatchDetails(eventId);
    } else {
        console.error("Invalid eventId in route params:", eventIdParam);
    }
});

// Also watch for route changes (e.g., when clicking sidebar links)
watch(() => $route.params.eventId, async (newVal) => {
    let eventId: number | undefined = undefined;
    if (typeof newVal === "string") {
        eventId = Number(newVal);
    } else if (Array.isArray(newVal) && newVal.length > 0) {
        eventId = Number(newVal[0]);
    }
    if (eventId && !isNaN(eventId)) {
        eventStore.setEvent(eventId);
        await loadMatchDetails(eventId);
    }
});

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

async function updateEvent() {
    if (selectedEvent.value.event.eventId) {
        //selectedEvent.value.event.eventName = await eventStore.updateEvent(selectedEvent.value.event);
        isEditing.value = false;
    } else {
        console.error("No event selected for update.");
    }
}

async function goBack() {
    await $router.push({ name: 'events' });
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

async function saveNewMatch(idx: number) {
  const match = newMatches.value[idx];
  const matchDetails = {
    match: match,
    teams: []
  };
  await createMatch(matchDetails);
  newMatches.value.splice(idx, 1);
}

function cancelNewMatch(idx: number) {
  newMatches.value.splice(idx, 1);
}
</script>

<template>
    <div class="relative flex flex-col h-full">
        <div class="sticky top-0 z-10 bg-white">
            <div class="relative w-full">
                <Button
                    @click="goBack"
                    icon="icon-arrow-left"
                    label="Back"
                    color="secondary"
                    class="absolute left-4 top-4"
                />
            </div>
            <div class="w-full text-center flex items-center justify-center py-6 text-6xl font-bold border-b border-gray-200">
              <div class="flex flex-col w-full items-center">
                <span v-if="!isEditing">
                  {{ selectedEvent.event.eventName }}
                </span>
                <span v-else>
                    <TextInput 
                        v-model="selectedEvent.event.eventName"
                        placeholder="Event Name"
                        class="text-4xl"
                    />
                </span>
                <span v-if="!isEditing" class="mt-0.5">
                  <span class="text-xl font-semibold text-gray-500">{{ selectedEvent.event.eventDate }}</span>
                </span>
                <span v-else class="mt-0.5">
                  <Datepicker
                    v-model="dateRange"
                    range
                    :multi-calendars="false"
                    :format="'yyyy-MM-dd'"
                    :placeholder="'Select date range'"
                    class="text-2xl"
                  />
                </span>
                  <div>
                    <span v-if="!isEditing" @click="isEditing = true" class="inline-flex items-center justify-center mt-2 rounded-lg border border-gray-300 bg-gray-100 hover:bg-gray-200 cursor-pointer transition-all duration-150 p-1">
                      <PencilIcon class="h-8 w-8 align-middle" />
                    </span>
                    <span v-else class="inline-flex items-center justify-center px-4 text-lg mt-2 pt-2 gap-x-2">
                      <Button @click="updateEvent()" color="secondary" label="Save" />
                      <Button @click="isEditing = false" color="secondary" label="Cancel" />
                    </span>
                  </div>
              </div>
            </div>
            <span @click="addNewMatch"
                  class="sticky top-0 z-20 bg-white flex w-full justify-center py-3 shadow-md border-b border-gray-200 cursor-pointer hover:bg-gray-100">
                <PlusCircleIcon class="h-12 w-12" />
                <span class="flex items-center text-2xl font-semibold ml-2">
                    Add Match
                </span>
            </span>
        </div>
        <div class="flex flex-col h-full">
          <div class="flex-1 min-h-0 overflow-y-auto rounded-md bg-white shadow">
            <ul role="list" class="divide-y divide-gray-200">
                <li v-if="_Matches.length > 1" v-for="detail in _Matches" class="px-6 py-4 hover:cursor-pointer hover:bg-gray-50" @click="openMatchDetails(detail.match.matchId)">
                    <div class="flex w-full" v-if="detail.match.matchName !== '' && detail.match.matchName">
                        <span class="w-1/3 flex items-center font-bold text-2xl whitespace-pre-wrap">
                            {{detail.match.matchName}}
                        </span>

                        <div class="w-2/3 flex justify-center">
                            <div class="mx-auto max-w-7xl w-full flex justify-center">
                                <div class="-mx-6 gap-1 overflow-hidden sm:mx-0 sm:rounded-2xl flex w-full justify-center items-center">
                                    <div v-for="team in detail.teams" class="bg-gray-200 rounded-lg p-4 hover:bg-green-200 text-center cursor-pointer font-semibold w-48 h-24 flex items-center justify-center break-words whitespace-pre-wrap" style="word-break: break-word;">
                                        {{team.teamName}}
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </li>
                <li v-else class="px-6 py-12 cursor-default">
                    <div class="w-full flex justify-center">
                        <div class="mx-auto max-w-7xl px-6 lg:px-8 w-full text-3xl flex justify-center">
                            <div class="-mx-6 gap-1 overflow-hidden text-gray-500 sm:mx-0 sm:rounded-2xl flex w-full text-center justify-center items-center">
                                    No matches found for this event.
                            </div>
                        </div>
                    </div>
                    
                </li>

                <!-- Render new matches being added -->
                <li v-for="(match, idx) in newMatches" :key="'new-' + idx" class="px-6 py-4 bg-yellow-50 flex w-full items-center gap-4 min-h-[112px]">
                  <TextInput
                      v-model="match.matchName"
                      placeholder="Enter match name..."
                      class="w-2/3 items-center font-bold text-2xl whitespace-pre-wrap rounded px-2 py-1"
                  />
                  <span class="text-gray-400 italic w-1/6 text-center">(new match)</span>
                  <div class="flex gap-2 ml-auto w-1/6">
                    <button @click="saveNewMatch(idx)" class="rounded bg-green-100 hover:bg-green-200 text-green-700 hover:text-green-900 p-2 flex items-center justify-center">
                      <svg xmlns="http://www.w3.org/2000/svg" class="h-7 w-7" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                        <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M5 13l4 4L19 7" />
                      </svg>
                    </button>
                    <button @click="cancelNewMatch(idx)" class="rounded bg-red-100 hover:bg-red-200 text-red-700 hover:text-red-900 p-2 flex items-center justify-center">
                      <svg xmlns="http://www.w3.org/2000/svg" class="h-7 w-7" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                        <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M6 18L18 6M6 6l12 12" />
                      </svg>
                    </button>
                  </div>
                </li>
            </ul>
          </div>
        </div>
    </div>
    
</template>