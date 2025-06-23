<script setup lang="ts">
import {ref, reactive, toRefs, onMounted} from 'vue'
import type {Team} from "@/types/team.ts";
import matchesStore, {selectedMatch, _Matches, _CurrentMatchTeams} from '@/stores/matches'
import { selectedEvent } from '@/stores/events';
import {PlusCircleIcon} from "@heroicons/vue/24/solid";
import { useRouter } from 'vue-router';
import Button from "@/components/General/Button.vue";
import TextInput from "@/components/Forms/TextInput.vue";

const newTeams = ref<Team[]>([])
const $router = useRouter();

// Track edit state for each team
const editStates = reactive<Record<number, {
    originalName: string;
    editing: boolean;
    value: string;
}>>({});

function addNewTeam(){
    const newTeam: Team = {
        teamId: 0,
        teamName: ''
    }
    newTeams.value.push(newTeam)
}
onMounted(async () => {
    
})
async function goBack() {
    await $router.push({ name: 'matches', params: { eventId: selectedEvent.value.event.eventId } });
}
function saveTeams() {
    // TODO: Implement save logic for teams
    // You can call an API or update the store here
    alert('Teams saved!');
}
function saveNewTeam(idx: number) {
    // TODO: Implement actual save logic (e.g., API call)
    // For now, just remove from newTeams
    newTeams.value.splice(idx, 1);
}
function cancelNewTeam(idx: number) {
    newTeams.value.splice(idx, 1);
}

// Team editing functions
function startEditTeam(team: Team) {
    if (!editStates[team.teamId]) {
        editStates[team.teamId] = {
            originalName: team.teamName,
            editing: true,
            value: team.teamName
        };
    } else {
        editStates[team.teamId].editing = true;
        editStates[team.teamId].value = team.teamName;
        editStates[team.teamId].originalName = team.teamName;
    }
}

function onTeamNameInput(team: Team) {
    if (editStates[team.teamId]) {
        editStates[team.teamId].editing = true;
        editStates[team.teamId].value = team.teamName;
    }
}

function saveEditTeam(team: Team) {
    // TODO: Implement save logic (API/store update)
    team.teamName = editStates[team.teamId].value;
    editStates[team.teamId].editing = false;
}

function cancelEditTeam(team: Team) {
    team.teamName = editStates[team.teamId].originalName;
    editStates[team.teamId].editing = false;
}
</script>

<template>
    <div class="relative">
        <div class="sticky top-0 z-10 bg-white">
            <span class="flex w-full justify-between">
               <Button
                   @click="goBack"
                   icon="icon-arrow-left"
                   label="Back"
                   color="secondary"
                   class="absolute left-4 top-4"
               />
            </span>
            <h1 class="w-full text-center flex items-center justify-center py-6 text-6xl font-bold border-b border-gray-200">
                {{ selectedMatch.match.matchName }}
            </h1>
            <span class="sticky top-0 z-20 bg-white flex w-full justify-center py-3 shadow-md border-b border-gray-200 cursor-pointer hover:bg-gray-100" 
                  @click="addNewTeam">
                <PlusCircleIcon class="h-12 w-12" />
            </span>
        </div>
        <ul role="list" class="divide-y divide-gray-200">
            <li v-for="team in _CurrentMatchTeams" :key="team.teamId" class="px-6 py-4 hover:cursor-pointer hover:bg-gray-50 flex justify-center items-center gap-4">
                <TextInput 
                    v-model="team.teamName"
                    @focus="startEditTeam(team)"
                    @input="onTeamNameInput(team)"
                />
                <template v-if="editStates[team.teamId] && editStates[team.teamId].editing && team.teamName !== editStates[team.teamId].originalName">
                    <div class="flex gap-2 ml-2">
                        <button @click.stop="saveEditTeam(team)" class="rounded bg-green-100 hover:bg-green-200 text-green-700 hover:text-green-900 p-2 flex items-center justify-center">
                            <svg xmlns="http://www.w3.org/2000/svg" class="h-7 w-7" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                                <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M5 13l4 4L19 7" />
                            </svg>
                        </button>
                        <button @click.stop="cancelEditTeam(team)" class="rounded bg-red-100 hover:bg-red-200 text-red-700 hover:text-red-900 p-2 flex items-center justify-center">
                            <svg xmlns="http://www.w3.org/2000/svg" class="h-7 w-7" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                                <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M6 18L18 6M6 6l12 12" />
                            </svg>
                        </button>
                    </div>
                </template>
            </li>
            <li v-for="(team, idx) in newTeams" :key="'new-' + idx" class="px-6 py-4 hover:cursor-pointer hover:bg-gray-50 flex justify-center items-center gap-4 bg-yellow-50 shadow-md">
                <input type="text" v-model="team.teamName" class="w-80 min-w-80 max-w-80 text-2xl font-bold text-center border py-4 break-words whitespace-pre-wrap" style="word-break: break-word;" />
                <div class="flex gap-2 ml-2">
                    <button @click.stop="saveNewTeam(idx)" class="rounded bg-green-100 hover:bg-green-200 text-green-700 hover:text-green-900 p-2 flex items-center justify-center">
                        <svg xmlns="http://www.w3.org/2000/svg" class="h-7 w-7" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                            <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M5 13l4 4L19 7" />
                        </svg>
                    </button>
                    <button @click.stop="cancelNewTeam(idx)" class="rounded bg-red-100 hover:bg-red-200 text-red-700 hover:text-red-900 p-2 flex items-center justify-center">
                        <svg xmlns="http://www.w3.org/2000/svg" class="h-7 w-7" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                            <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M6 18L18 6M6 6l12 12" />
                        </svg>
                    </button>
                </div>
            </li>
        </ul>
    </div>
</template>
