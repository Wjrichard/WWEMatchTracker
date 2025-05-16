<script setup lang="ts">
import {ref} from 'vue'
import type {Team} from "@/types/team.ts";
import matchesStore, {selectedMatch, loadMatchDetails, _Matches, _CurrentMatchTeams} from '@/stores/matches'
import {onMounted} from "vue";
import {PlusCircleIcon} from "@heroicons/vue/24/solid";

const newTeams = ref<Team[]>([])

function addNewTeam(){
    const newTeam: Team = {
        teamId: 0,
        teamName: ''
    }
    newTeams.value.push(newTeam)
}
onMounted(async () => {
    
})
</script>

<template>
    <div class="sticky top-0 z-10 bg-white">
        <h1 class="w-full text-center flex items-center justify-center py-12 text-8xl font-bold">
            {{ selectedMatch.match.matchName }}
        </h1>
    </div>
    <ul role="list" class="divide-y divide-gray-200">
        <li v-for="team in _CurrentMatchTeams" class="px-6 py-4 hover:cursor-pointer hover:bg-gray-50">
            <input type="text" v-model="team.teamName" class="w-full text-2xl font-bold text-center border py-4" />
        </li>
        <li v-for="team in newTeams" class="px-6 py-4 hover:cursor-pointer hover:bg-gray-50">
            <input type="text" v-model="team.teamName" class="w-full text-2xl font-bold text-center border py-4" />
        </li>
        <li class="flex w-full justify-center py-6 hover:bg-gray-100 cursor-pointer" @click="addNewTeam">
            <PlusCircleIcon class="h-15 w-15" />
        </li>
    </ul>
</template>
