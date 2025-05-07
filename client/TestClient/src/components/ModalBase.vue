<script setup lang="ts">
import { ref, watch, computed } from 'vue'
import { Dialog, DialogPanel, DialogTitle, TransitionChild, TransitionRoot } from '@headlessui/vue'
import type { ModalResponseType } from '@/code/Modal'
import Button from '@/components/ButtonComponent.vue'

const props = defineProps<{
    open: boolean
    size?: 'extra-small' | 'small' | 'normal' | 'large' | 'extra-large' | '2x-large'
    fullHeight?: boolean
}>()

const computedStyles = computed(() => {
    let tailwindMaxWidth: string

    switch (props.size) {
        case 'extra-small':
            tailwindMaxWidth = 'sm:max-w-lg'
            break
        case 'small':
            tailwindMaxWidth = 'sm:max-w-2xl'
            break
        case 'large':
            tailwindMaxWidth = 'sm:max-w-6xl'
            break
        case 'extra-large':
            tailwindMaxWidth = 'sm:max-w-7xl'
            break
        case '2x-large':
            tailwindMaxWidth = 'sm:max-w-screen-2xl'
            break

        case 'normal':
        default:
            tailwindMaxWidth = 'sm:max-w-4xl'
    }

    return { size: tailwindMaxWidth }
})
const emits = defineEmits<{
    (e: 'close', responseData: ModalResponseType): void
}>()

const open = ref(props.open)
watch(
    () => props.open,
    (openState) => {
        open.value = openState
    }
)

function handleClose() {
    open.value = false
    emits('close', 'acknowledge')
}
</script>

<template>
    <TransitionRoot as="template" :show="open">
        <Dialog as="div" class="relative z-50" @close="handleClose">
            <TransitionChild
                as="template"
                enter="ease-out duration-300"
                enter-from="opacity-0"
                enter-to="opacity-100"
                leave="ease-in duration-200"
                leave-from="opacity-100"
                leave-to="opacity-0"
            >
                <div class="fixed inset-0 bg-gray-500 bg-opacity-75 transition-opacity" />
            </TransitionChild>

            <div class="fixed inset-0 z-10 overflow-y-auto">
                <div
                    class="flex min-h-full items-end justify-center p-4 text-center sm:items-center sm:p-0"
                >
                    <TransitionChild
                        as="template"
                        enter="ease-out duration-300"
                        enter-from="opacity-0 translate-y-4 sm:translate-y-0 sm:scale-95"
                        enter-to="opacity-100 translate-y-0 sm:scale-100"
                        leave="ease-in duration-200"
                        leave-from="opacity-100 translate-y-0 sm:scale-100"
                        leave-to="opacity-0 translate-y-4 sm:translate-y-0 sm:scale-95"
                    >
                        <DialogPanel
                            class="relative transform rounded-lg bg-white px-4 pb-4 pt-5 text-left shadow-xl transition-all sm:my-8 sm:w-full sm:p-6"
                            :class="[
                computedStyles.size,
                props.fullHeight ? 'flex h-[85vh] flex-col overflow-auto' : ''
              ]"
                        >
                            <div class="flex space-x-3">
                                <slot name="icon" />
                                <DialogTitle
                                    as="h3"
                                    class="flex w-full items-center justify-between text-lg font-medium leading-6 text-gray-900"
                                >
                                    <slot name="header" />
                                </DialogTitle>
                            </div>
                            <div class="mt-2" :class="$props.fullHeight && 'h-full'">
                                <slot v-if="$slots.body" name="body" />
                                <slot v-else />
                            </div>
                            <div
                                class="mt-4 flex flex-row-reverse justify-between gap-3"
                                :class="[props.fullHeight]"
                            >
                                <div class="flex flex-row gap-x-2">
                                    <slot name="footer">
                                        <Button color="blue" primary @click="handleClose">Close</Button>
                                    </slot>
                                </div>
                                <slot name="footer-left" />
                            </div>
                        </DialogPanel>
                    </TransitionChild>
                </div>
            </div>
        </Dialog>
    </TransitionRoot>
</template>
