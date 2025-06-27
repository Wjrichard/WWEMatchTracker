<script setup lang="ts">
import {ref, reactive, toRefs, onMounted} from 'vue'
import type {Team} from "@/types/team.ts";
import matchesStore, {selectedMatch, _Matches, _CurrentMatchTeams} from '@/stores/matches'
import { selectedEvent } from '@/stores/events';
import {PlusCircleIcon} from "@heroicons/vue/24/solid";
import { useRouter } from 'vue-router';
import Button from "@/components/General/Button.vue";
import TextInput from "@/components/Forms/TextInput.vue";
import Combobox from "@/components/Forms/Combobox.vue";
import teamsStore from '@/stores/teams'

const newTeams = ref<Team[]>([])
const $router = useRouter();
const { _Teams, loadTeams } = teamsStore

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
    await loadTeams();
    // Always unwrap .value for refs, fallback to [] if not an array
    const teamsArr: Team[] = Array.isArray((_CurrentMatchTeams as any).value)
        ? (_CurrentMatchTeams as any).value
        : Array.isArray(_CurrentMatchTeams)
            ? _CurrentMatchTeams as Team[]
            : [];
    const allTeamsArr: Team[] = Array.isArray((_Teams as any).value)
        ? (_Teams as any).value
        : Array.isArray(_Teams)
            ? _Teams as Team[]
            : [];
    teamsArr.forEach((team: Team) => {
        // Only set teamName if it is empty or not matching the correct team
        if (!team.teamName || !allTeamsArr.some(t => t.teamName === team.teamName)) {
            const found = allTeamsArr.find((t: Team) => t.teamId === team.teamId);
            if (found) {
                team.teamName = found.teamName;
            }
        }
    });
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

// Add these methods to the <script setup> section:
function getTeamObject(teamName: string, teams: Team[] | any): Team | undefined {
    const arr = Array.isArray((teams as any).value) ? (teams as any).value : teams;
    return arr.find((t: Team) => t.teamName === teamName);
}
function setTeamFromCombo(team: Team, val: Team) {
    if (val) {
        team.teamName = val.teamName;
        team.teamId = val.teamId;
    }
}
</script>

<template>
    <div class="relative">
        <div class="sticky top-0 z-10 bg-white">
            <div class="w-full flex items-start justify-between py-6 px-4 border-b border-gray-200">
                <div class="flex items-start pt-1">
                  <Button
                      @click="goBack"
                      icon="icon-arrow-left"
                      label="Back"
                      color="secondary"
                  />
                </div>
                <div class="flex flex-col w-full items-center">
                  <span class="text-6xl font-bold">
                    {{ selectedMatch.match.matchName }}
                  </span>
                </div>
                <div class="w-24"></div>
            </div>
            <span class="sticky top-0 z-20 bg-white flex w-full justify-center py-3 shadow-md border-b border-gray-200 cursor-pointer hover:bg-gray-100" 
                  @click="addNewTeam">
                <PlusCircleIcon class="h-12 w-12" />
            </span>
        </div>
        <ul role="list" class="divide-y divide-gray-200">
            <li v-for="team in _CurrentMatchTeams" :key="team.teamId" class="px-6 py-4 hover:cursor-pointer hover:bg-gray-50 flex justify-center items-center gap-4">
                <Combobox
                    :model-value="getTeamObject(team.teamName, _Teams)"
                    @update:model-value="val => setTeamFromCombo(team, val)"
                    :options="_Teams"
                    placeholder="Select team..."
                    :option-context="{ key: 'teamId', displayKey: 'teamName', valueKey: 'teamName' }"
                    class="w-1/3 text-2xl font-bold text-center border py-4 break-words whitespace-pre-wrap"
                />
            </li>
        </ul>
    </div>
</template>


