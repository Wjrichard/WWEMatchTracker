<script lang="ts">
    import userStore from '@/stores/user.ts'
    import {ref} from 'vue'
    await userStore.loadUsers()
    const strEmail = ref('')
</script>

<template>
    <div class="flex min-h-full flex-1 items-center justify-center px-4 py-12 sm:px-6 lg:px-8">
        <div class="w-full max-w-sm space-y-10">
            <div>
                <h2 class="mt-10 text-center text-2xl/9 font-bold tracking-tight text-gray-900">Sign in</h2>
            </div>
            <form class="space-y-6" action="#" method="POST">
                <div>
                    <div class="pt-2">
                        <input id="email-address" 
                               name="email" 
                               type="email" 
                               v-model="strEmail"
                               autocomplete="email" 
                               aria-label="Email address" 
                               class="block w-full rounded-t-md bg-white px-3 py-1.5 text-base text-gray-900 outline outline-1 -outline-offset-1 outline-gray-300 placeholder:text-gray-400 focus:relative focus:outline focus:outline-2 focus:-outline-offset-2 focus:outline-indigo-600 sm:text-sm/6" placeholder="Email address" />
                    </div>
                </div>

                <div class="pt-3">
                    <button @click=""
                            class="flex w-full justify-center rounded-md bg-indigo-600 px-3 py-1.5 text-sm/6 
                                    font-semibold text-white hover:bg-indigo-500 focus-visible:outline focus-visible:outline-2 
                                    focus-visible:outline-offset-2 focus-visible:outline-indigo-600"
                    >
                        Sign in</button>
                </div>
            </form>
        </div>
    </div>
</template>
