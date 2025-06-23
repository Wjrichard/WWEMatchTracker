import type { Team } from '@/types/team.ts'
import { ref, computed, watch } from 'vue'
import axios from 'axios'

export const initializedTeam = {
    teamId: 0,
    teamName: ''
}

export const _Teams = ref<Team[]>([])

// Load from localStorage if present
const savedSelectedTeam = localStorage.getItem('selectedTeam')
export const selectedTeam = ref<Team>(
    savedSelectedTeam ? JSON.parse(savedSelectedTeam) : initializedTeam
)

export async function saveTeam(team: Team) {
    const newTeam = { ...initializedTeam, teamName: team.teamName }
    await axios.post('https://localhost:44328/Team/SaveTeam', newTeam)
}

export async function loadTeams() {
    try {
        const response = await axios.get('https://localhost:44328/Teams/GetTeams')
        _Teams.value = response.data
        console.log('Teams loaded:', _Teams.value)
    } catch (error) {
        console.error('Error fetching teams:', error)
    }
}

const teamsStore = { _Teams, selectedTeam, initializedTeam, loadTeams, saveTeam };
export default teamsStore;