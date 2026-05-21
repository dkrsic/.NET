(function () {
	function initAjaxAutocomplete() {
		const selects = document.querySelectorAll("select.js-ajax-autocomplete[data-autocomplete-url]");

		selects.forEach(function (select) {
			if (select.dataset.autocompleteInitialized === "true") {
				return;
			}

			const url = select.dataset.autocompleteUrl;
			const placeholder = select.dataset.autocompletePlaceholder || "Pretrazi...";

			if (!url) {
				return;
			}

			select.dataset.autocompleteInitialized = "true";

			const wrapper = document.createElement("div");
			wrapper.className = "ajax-autocomplete";

			const input = document.createElement("input");
			input.type = "search";
			input.className = "form-control ajax-autocomplete-input";
			input.placeholder = placeholder;
			input.autocomplete = "off";

			const list = document.createElement("div");
			list.className = "ajax-autocomplete-list";
			list.hidden = true;

			select.parentNode.insertBefore(wrapper, select);
			wrapper.appendChild(input);
			wrapper.appendChild(list);
			wrapper.appendChild(select);

			select.classList.add("d-none");

			if (select.selectedIndex >= 0 && select.options[select.selectedIndex]) {
				const selectedOption = select.options[select.selectedIndex];
				if (selectedOption.value) {
					input.value = selectedOption.text;
				}
			}

			let timer = null;
			let abortController = null;

			function syncValidationClass() {
				const invalid = select.classList.contains("input-validation-error")
					|| select.getAttribute("aria-invalid") === "true";
				input.classList.toggle("input-validation-error", invalid);
			}

			function closeList() {
				list.hidden = true;
				list.innerHTML = "";
			}

			function setValue(id, text) {
				let option = Array.from(select.options).find(function (o) {
					return o.value === String(id);
				});

				if (!option) {
					option = document.createElement("option");
					option.value = String(id);
					option.text = text;
					select.appendChild(option);
				}

				option.selected = true;
				select.value = String(id);
				input.value = text;
				closeList();

				select.dispatchEvent(new Event("change", { bubbles: true }));
				select.dispatchEvent(new Event("blur", { bubbles: true }));
				setTimeout(syncValidationClass, 0);
			}

			function renderItems(items) {
				list.innerHTML = "";

				if (!items || items.length === 0) {
					const empty = document.createElement("div");
					empty.className = "ajax-autocomplete-empty";
					empty.textContent = "Nema rezultata";
					list.appendChild(empty);
					list.hidden = false;
					return;
				}

				items.forEach(function (item) {
					const button = document.createElement("button");
					button.type = "button";
					button.className = "ajax-autocomplete-item";
					button.textContent = item.text;
					button.addEventListener("click", function () {
						setValue(item.id, item.text);
					});
					list.appendChild(button);
				});

				list.hidden = false;
			}

			async function loadItems(query) {
				if (abortController) {
					abortController.abort();
				}

				abortController = new AbortController();

				try {
					const response = await fetch(url + "?q=" + encodeURIComponent(query || ""), {
						headers: { "X-Requested-With": "XMLHttpRequest" },
						signal: abortController.signal
					});

					if (!response.ok) {
						closeList();
						return;
					}

					const items = await response.json();
					renderItems(items);
				} catch (error) {
					if (error && error.name === "AbortError") {
						return;
					}
					closeList();
				}
			}

			input.addEventListener("input", function () {
				clearTimeout(timer);

				if (!input.value.trim()) {
					select.value = "";
					select.dispatchEvent(new Event("change", { bubbles: true }));
					setTimeout(syncValidationClass, 0);
				}

				timer = setTimeout(function () {
					loadItems(input.value.trim());
				}, 220);
			});

			input.addEventListener("focus", function () {
				loadItems(input.value.trim());
			});

			input.addEventListener("blur", function () {
				setTimeout(function () {
					if (!wrapper.contains(document.activeElement)) {
						closeList();
						if (!input.value.trim()) {
							select.value = "";
							select.dispatchEvent(new Event("change", { bubbles: true }));
							select.dispatchEvent(new Event("blur", { bubbles: true }));
						}
						setTimeout(syncValidationClass, 0);
					}
				}, 120);
			});

			document.addEventListener("click", function (event) {
				if (!wrapper.contains(event.target)) {
					closeList();
				}
			});

			select.addEventListener("change", syncValidationClass);
			select.addEventListener("blur", function () {
				setTimeout(syncValidationClass, 0);
			});

			const form = select.closest("form");
			if (form) {
				form.addEventListener("submit", function () {
					setTimeout(syncValidationClass, 0);
				});
			}
		});
	}

	if (document.readyState === "loading") {
		document.addEventListener("DOMContentLoaded", initAjaxAutocomplete);
	} else {
		initAjaxAutocomplete();
	}
})();
