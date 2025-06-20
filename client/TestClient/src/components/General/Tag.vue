<script setup lang="ts">
import { twMerge } from "tailwind-merge";
import type { IconClassName } from "@/components/utilities/Icons.ts";
import type { ItemProps } from "../LayoutHelpers/Item.vue";
import Item from "../LayoutHelpers/Item.vue";
import type { Colors, Variants } from "../Styles/DefaultGenerators";
import Icon from "./Icon.vue";
import {
  generateSecondaryColorClasses,
  generateColorClasses,
} from "../Styles/DefaultGenerators";
import { getCurrentInstance, ref, type Ref } from "vue";

/**
 * Props for the Tag component.
 */
export type TagProps = {
  /** The text label displayed inside the tag. */
  label?: string;
  /** Optional icon to display alongside the label. */
  icon?: IconClassName;
  /** Optional item data associated with the tag. */
  item?: ItemProps;
  /** Optional disabled prop to prevent interaction */
  disabled?: boolean;
  /** The color style of the tag. */
  color?: "primary" | "secondary" | Colors;
  /** The visual variant of the tag. */
  variant?: Variants;
  /** If true, displays a close button to remove the tag. */
  closable?: boolean;
  /** If true, applies hover effects to the tag. */
  hoverable?: boolean;
};

export type TagEmits = {
  /**
   * Emits when the tag is requested to be closed.
   * Handling and setting `shouldClose.value = true` will cancel the close action.
   * @event
   * @param {'close'} e - The close event.
   * @param {Ref<boolean>} shouldClose - A ref that can be set to false to prevent closing.
   * @returns {void}
   */
  (e: "close", shouldClose: Ref<boolean, boolean>): void;
};

const props = defineProps<TagProps>();
const emits = defineEmits<TagEmits>();

const hidden = ref(false);
function closeTag() {
  let shouldClose = ref(true);
  emits("close", shouldClose);
  if (shouldClose.value === false) {
    return;
  }
  hidden.value = true;
}
</script>

<template>
  <component
    :is="hoverable || disabled ? 'button' : 'div'"
    v-if="!hidden"
    :disabled="disabled"
    :class="[
      twMerge(
        `flex w-fit items-center gap-2 rounded-md px-1.5 py-0.5 text-sm font-medium select-none disabled:cursor-not-allowed disabled:opacity-75`,
        hoverable ? 'cursor-pointer' : '',
        color === 'secondary'
          ? generateSecondaryColorClasses(color, false, hoverable)
          : generateColorClasses(color ?? 'primary', variant, false, hoverable),
      ),
    ]"
  >
    <Item v-bind="item" v-if="item" />
    <template v-else>
      <span v-if="label">{{ label }}</span>
      <Icon v-if="icon" :name="icon" />
    </template>
    <button v-if="closable" aria-label="Close">
      <Icon
        name="icon-x"
        :class="[disabled ? 'cursor-not-allowed' : 'cursor-pointer']"
        @click="!disabled && closeTag()"
      />
    </button>
  </component>
</template>
