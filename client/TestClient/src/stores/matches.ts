import type { Match } from '@/types/match.ts'
import type { MatchDetails } from "@/types/matchDetails.ts"
import type { Team } from '@/types/team.ts'
import {ref, computed} from 'vue'
import axios from "axios";

export const initializedMatch = {
    match: {
        matchId: 0,
        matchName:''
    },
    teams: []
}

export const _Matches = ref<MatchDetails[]>([]);
export const _CurrentMatchTeams = computed(() => {
    return selectedMatch.value?.teams
})

// Load from localStorage if present
const savedSelectedMatch = localStorage.getItem('selectedMatch');
export const selectedMatch = ref<MatchDetails>(
  savedSelectedMatch ? JSON.parse(savedSelectedMatch) : initializedMatch
);


export async function createMatch(match:MatchDetails){
    const newMatch = initializedMatch
    newMatch.match.matchName = match.match.matchName
    const response = await axios.post('https://localhost:44328/Match/AddMatch',newMatch.match)
}

export async function loadMatchDetails(eventId:number) {
    try {
        const response = await axios.get(`https://localhost:44328/Match/GetMatchDetailsForEvent/${eventId}`);
        _Matches.value = response.data;
    } catch (error) {
        console.error('Error fetching users:', error);
    }
}

import { watch } from 'vue';
watch(selectedMatch, (val) => {
  localStorage.setItem('selectedMatch', JSON.stringify(val));
}, { deep: true });

export function setMatch(matchId:number) {
    const curMatch = _Matches.value?.find(detail => detail.match.matchId === matchId)
    if (curMatch){
        selectedMatch.value = curMatch
        // Persist to localStorage
        localStorage.setItem('selectedMatch', JSON.stringify(curMatch));
    }
}

const matchesStore = { _Matches, selectedMatch, loadMatchDetails, initializedMatch, setMatch, _CurrentMatchTeams, createMatch };
export default matchesStore;
