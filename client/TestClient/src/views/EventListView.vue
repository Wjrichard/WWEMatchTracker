<script setup lang="ts">
import eventsStore,{_Events} from "@/stores/events.ts";
import ComboBox from "@/components/Layout/ComboBox.vue";
import type {Event} from "@/types/event.ts";
import {ChevronUpDownIcon} from "@heroicons/vue/24/solid";
import {ref} from 'vue'

const selectedEvent = ref(Event)
</script>

<template>
    <div class="overflow-auto pt-12 w-11/12 justify-center bg-white">
        <div class="w-full h-24 flex items-center border-gray-100 rounded-md shadow-sm bg-gray-50 sticky top-0 sticky-top z-50">
          <span class="w-full flex items-center px-2 py-2 justify-center">
              <ComboBox
                  placeholder="Select an Event"
                  class="p-4 text-2xl w-1/2 flex justify-center"
                  :options="_Events"
                  :option-context="{
                      key: 'eventId',
                      displayKey: 'eventName',
                      valueKey: 'eventId',
                  }"
                  v-model="selectedEvent"
              />
          </span>
            
        </div>
        <ul role="list" class="divide-y divide-gray-200 h-full">
            <li v-for="e in _Events" :key="e.eventId" class="px-6 py-4">
                <div class="flex w-full">
                    <span class="w-1/2 flex items-center justify-center border rounded-md">
                        Event Image Here
                    </span>
                    <div class="w-1/3">
                        <div class="flex w-full">
                            <span class="flex w-1/2 rounded-md px-2 justify-end">
                                Event Name:
                            </span>
                            <span class="flex w-1/2 rounded-md px-2 justify-center">
                                {{e.eventName}}
                            </span>
                        </div>
                        <div class="flex w-full">
                            <span class="flex w-1/2 py-2 rounded-md px-2 justify-end">
                                Date:
                            </span>
                            <span class="flex w-1/2 py-2 rounded-md px-2 justify-center">
                                {{e.eventDate}}
                            </span>
                        </div>
                    </div>
                </div>
            </li>
        </ul>
    </div>
</template>
