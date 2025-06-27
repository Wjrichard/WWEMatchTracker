<script setup lang="ts">
import { onMounted, ref } from 'vue'
import teamsStore from '@/stores/teams'
import TextInput from "@/components/Forms/TextInput.vue";
import Button from "@/components/General/Button.vue";

const { _Teams, loadTeams } = teamsStore
const teamSearch = ref('')
const newUserName = ref('')

const editStates = ref<Record<number, { editing: boolean; value: string; original: string }>>({});

onMounted(async () => {
  await loadTeams()
})

function addUser() {
  // Logic to add a new user
}

function startEditTeam(team: any) {
  editStates.value[team.teamId] = {
    editing: true,
    value: team.teamName,
    original: team.teamName,
  };
}

function saveEditTeam(team: any) {
  if (editStates.value[team.teamId]) {
    team.teamName = editStates.value[team.teamId].value;
    editStates.value[team.teamId].editing = false;
  }
}

function cancelEditTeam(team: any) {
  if (editStates.value[team.teamId]) {
    editStates.value[team.teamId].value = editStates.value[team.teamId].original;
    editStates.value[team.teamId].editing = false;
  }
}
</script>

<template>
  <div class="min-h-full h-full flex flex-col items-center p-8 overflow-hidden">
    <div class="w-1/2 flex flex-col h-full">
      <div class="sm:flex sm:items-center">
        <div class="sm:flex-auto flex justify-center items-center">
          <h1 class="text-6xl font-semibold text-gray-900">Teams</h1>
        </div>
      </div>
      <div class="w-full flex flex-col items-center justify-center px-8 pt-8">
          <form class="flex w-full flex-row gap-4 items-end" @submit.prevent="addUser">
            <TextInput 
              v-model="newUserName"
              placeholder="Enter new team..."
              class="mb-2 flex-1"
            />
            <Button 
              label="Add"
              color="info"
              icon="icon-plus"
              type="submit"
              class="mb-2"
            />
          </form>
      </div>
      <div class="mt-8 flex-1 flex flex-col pt-4">
        <div class="flex w-full bg-white border-b border-gray-200 sticky top-0 z-10">
          <span class="py-3.5 pl-4 pr-3 text-left text-xl font-semibold text-gray-900 sm:pl-3 w-1/3 flex items-center justify-cent">
              Name
          </span>
          <span class="relative py-3.5 pl-3 pr-4 sm:pr-3 w-2/3 justify-center">
            <TextInput
              v-model="teamSearch"
              placeholder="Search teams..."
            />
          </span>
        </div>
        <div class="flex-1 overflow-y-auto max-h-[60vh]">
          <div class="-mx-4 -my-2 overflow-x-auto sm:-mx-6 lg:-mx-8">
            <div class="inline-block min-w-full py-2 align-middle sm:px-6 lg:px-8">
              <div v-for="(team, idx) in _Teams" :key="team.teamId" class="even:bg-gray-50 flex">
                <span class="whitespace-nowrap py-4 pl-4 pr-3 text-lg font-medium text-gray-900 sm:pl-3 w-1/2">
                  <template v-if="editStates[team.teamId]?.editing">
                    <TextInput
                      v-model="editStates[team.teamId].value"
                      class="w-full"
                      @keydown.enter="saveEditTeam(team)"
                      @keydown.esc="cancelEditTeam(team)"
                    />
                  </template>
                  <template v-else>
                    {{ team.teamName }}
                  </template>
                </span>
                <span class="relative whitespace-nowrap py-4 pl-3 pr-4 text-right text-sm font-medium sm:pr-3 w-1/2 justify-end flex gap-2">
                  <template v-if="editStates[team.teamId]?.editing">
                    <Button
                      label="Save"
                      icon="icon-check"
                      color="secondary"
                      @click="saveEditTeam(team)"
                    />
                    <Button
                      label="Cancel"
                      icon="icon-x"
                      color="danger"
                      @click="cancelEditTeam(team)"
                    />
                  </template>
                  <template v-else>
                    <Button
                      label="Edit"
                      icon="icon-pencil"
                      color="secondary"
                      @click="startEditTeam(team)"
                    />
                    <Button
                      label="Delete"
                      icon="icon-x"
                      color="danger"
                      @click="() => {}"
                    />
                  </template>
                </span>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>
