<script setup lang="ts">
import {
  DialogClose,
  DialogContent,
  DialogDescription,
  DialogOverlay,
  DialogPortal,
  DialogRoot,
  DialogTitle,
  DialogTrigger,
} from "reka-ui";
import { ref, watch, computed } from "vue";
import Item, { type ItemProps } from "../LayoutHelpers/Item.vue";
import type { IconClassName } from "@/components/utilities/Icons.ts";
import Button from "../General/Button.vue";

const props = defineProps<{
  open?: boolean;
  size?: "xs" | "sm" | "md" | "lg" | "xl" | "2xl";
  closeButtonText?: string;
  hideCloseButton?: boolean;
  header?: string;
  icon?: IconClassName;
  itemProps?: ItemProps;
  isSaveCancel?: boolean;
  saveRouteName?: string;
}>();

const emits = defineEmits<{
  (e: "close"): void;
    (e: "save"): void;
}>();

const open = ref(props.open);
watch(
  () => props.open,
  (newOpen) => {
    open.value = newOpen;
  },
);

function handleSave() {
  open.value = false;
    emits("save");
}

function handleClose() {
  open.value = false;
  emits("close");
}

const sizeClasses = computed(() => {
  switch (props.size) {
    case "xs":
      return "min-w-[20vw]";
    case "sm":
      return "min-w-[25vw]";
    case "lg":
      return "min-w-[50vw]";
    case "xl":
      return "min-w-[66vw]";
    case "2xl":
      return "min-w-[80vw]";
    case "md":
    default:
      return "min-w-[33vw]";
  }
});
</script>

<template>
  <DialogRoot
    v-model:open="open"
    @update:open="
      (val) => {
        if (!val) handleClose();
      }
    "
  >
    <DialogTrigger v-if="$slots.trigger">
      <slot name="trigger" />
    </DialogTrigger>
    <DialogPortal>
      <DialogOverlay class="DialogOverlay" @click.self="handleClose">
        <DialogContent
          class="DialogContent flex flex-col rounded-lg bg-white shadow-lg"
          :class="sizeClasses"
        >
          <div class="flex-none p-2">
            <DialogTitle>
              <slot name="header">
                <h2 class="text-lg font-semibold">
                  <Item :icon-left="icon" :label="header" v-bind="itemProps" />
                </h2>
              </slot>
            </DialogTitle>
          </div>
          <DialogDescription
            class="flex flex-col items-center justify-center p-2"
          >
            <slot name="body" />
          </DialogDescription>
          <div class="p-2">
            <slot name="footer" />
            <div class="flex w-full justify-end">
              <DialogClose v-if="!hideCloseButton">
                  <div v-if="props.isSaveCancel" class="flex w-full justify-end gap-4 space-x-2">
                    <Button
                      color="secondary"
                      :label="props.closeButtonText ?? 'Save'"
                    />
                    <Button
                      color="secondary"
                      :label="'Cancel'"
                      @click="handleClose"
                    />
                  </div>
                  <div v-else>
                      <Button
                          class="text-white"
                          color="primary"
                          :label="props.closeButtonText ?? 'Close'"
                      />
                  </div>
              </DialogClose>
            </div>
          </div>
        </DialogContent>
      </DialogOverlay>
    </DialogPortal>
  </DialogRoot>
</template>

<style>
.DialogOverlay {
  background: rgba(0 0 0 / 0.5);
  position: fixed;
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
  display: grid;
  place-items: center;
  overflow-y: auto;
}

.DialogContent {
  background: white;
  padding: 30px;
  border-radius: 4px;
}
</style>
