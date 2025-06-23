<script setup lang="ts">
import { onMounted, ref } from 'vue'
import teamsStore from '@/stores/teams'
import TextInput from "@/components/Forms/TextInput.vue";
import Button from "@/components/General/Button.vue";

const { _Teams, loadTeams } = teamsStore
const teamSearch = ref('')
const newUserName = ref('')

onMounted(async () => {
  await loadTeams()
})

function addUser() {
  // Logic to add a new user
}
</script>

<template>
    <div class="p-8 w-full flex justify-center">
        <div class="w-1/2 h-full">
            <div class="sm:flex sm:items-center">
                <div class="sm:flex-auto flex justify-center items-center">
                    <h1 class="text-6xl font-semibold text-gray-900">Teams</h1>
                </div>
                <div class="mt-4 sm:ml-16 sm:mt-0 sm:flex-none">
                    <Button 
                        label="Add Team"
                        color="secondary"
                        icon="icon-plus"
                    />
                </div>
            </div>
            <div class="mt-8 flow-root overflow-auto">
                <div class="-mx-4 -my-2 overflow-x-auto sm:-mx-6 lg:-mx-8">
                    <div class="inline-block min-w-full py-2 align-middle sm:px-6 lg:px-8">
                            <div class="flex w-full">
                                <span class="py-3.5 pl-4 pr-3 text-left text-xl font-semibold text-gray-900 sm:pl-3 w-1/3">Name</span>
                                <span class="relative py-3.5 pl-3 pr-4 sm:pr-3 w-2/3 justify-center">
                                    <TextInput 
                                        v-model="teamSearch"
                                        placeholder="Search teams..."
                                    />
                                </span>
                            </div>
                            <div v-for="team in _Teams" :key="team.teamId" class="even:bg-gray-50 flex">
                                <span class="whitespace-nowrap py-4 pl-4 pr-3 text-sm font-medium text-gray-900 sm:pl-3 w-1/2">
                                    {{ team.teamName }}
                                </span>
                                <span class="relative whitespace-nowrap py-4 pl-3 pr-4 text-right text-sm font-medium sm:pr-3 w-1/2 justify-end flex gap-2">
                                    <Button 
                                        label="Edit"
                                        icon="icon-pencil"
                                        color="secondary"
                                        @click="() => {}"
                                    />
                                    <Button 
                                        label="Deactivate"
                                        icon="icon-x"
                                        color="secondary"
                                        @click="() => {}"
                                    />
                                </span>
                            </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="w-1/2 h-full flex flex-col items-center justify-start">
            <div class="w-full bg-white rounded-lg shadow p-6 mt-8">
                <h2 class="text-2xl font-semibold mb-4">Add User</h2>
                <form class="flex flex-col gap-4" @submit.prevent="addUser">
                    <TextInput 
                        v-model="newUserName"
                        placeholder="Enter new user name..."
                        class="mb-2"
                    />
                    <Button 
                        label="Add User"
                        color="primary"
                        icon="icon-plus"
                        type="submit"
                    />
                </form>
            </div>
        </div>
    </div>
</template>
