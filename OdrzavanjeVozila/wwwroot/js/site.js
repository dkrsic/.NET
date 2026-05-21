(function () {
	function initPageTransitions() {
		document.documentElement.classList.add("js-enabled");

		requestAnimationFrame(function () {
			document.body.classList.add("page-is-ready");
		});

		document.addEventListener("click", function (event) {
			if (!(event.target instanceof Element)) {
				return;
			}

			const anchor = event.target.closest("a[href]");
			if (!anchor || anchor.target === "_blank" || anchor.hasAttribute("download") || anchor.dataset.noPageTransition === "true") {
				return;
			}

			const href = anchor.getAttribute("href") || "";
			if (!href || href.startsWith("#") || href.startsWith("javascript:")) {
				return;
			}

			if (anchor.origin !== window.location.origin) {
				return;
			}

			document.body.classList.add("page-is-leaving");
		});

		document.addEventListener("submit", function (event) {
			const form = event.target;
			if (!(form instanceof HTMLFormElement)) {
				return;
			}

			if (form.dataset.noPageTransition === "true") {
				return;
			}

			document.body.classList.add("page-is-leaving");
		});
	}

	function initToasts() {
		const toastNodes = document.querySelectorAll(".app-toast");
		if (!toastNodes.length || !window.bootstrap || !bootstrap.Toast) {
			return;
		}

		toastNodes.forEach(function (node) {
			const toast = bootstrap.Toast.getOrCreateInstance(node);
			toast.show();
		});
	}

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

	function initAjaxTableSearches() {
		const searchInputs = document.querySelectorAll("input[data-search-url]");

		searchInputs.forEach(function (input) {
			if (input.dataset.searchInitialized === "true") {
				return;
			}

			const searchUrl = input.dataset.searchUrl;
			const rowsTargetId = input.dataset.rowsTarget || (input.id ? input.id.replace("Search", "Rows") : "");
			const rowsTarget = document.getElementById(rowsTargetId);
			if (!searchUrl || !rowsTarget) {
				return;
			}

			input.dataset.searchInitialized = "true";

			const tableWrapper = rowsTarget.closest(".glass-table");
			let timer = null;
			let abortController = null;

			function setLoading(isLoading) {
				if (!tableWrapper) {
					return;
				}

				tableWrapper.classList.toggle("is-loading", isLoading);
				rowsTarget.setAttribute("aria-busy", String(isLoading));
			}

			async function loadRows(query) {
				if (abortController) {
					abortController.abort();
				}

				abortController = new AbortController();
				setLoading(true);

				try {
					const response = await fetch(searchUrl + "?q=" + encodeURIComponent(query || ""), {
						headers: { "X-Requested-With": "XMLHttpRequest" },
						signal: abortController.signal
					});

					if (!response.ok) {
						return;
					}

					rowsTarget.innerHTML = await response.text();
				} catch (error) {
					if (error && error.name === "AbortError") {
						return;
					}
				} finally {
					setLoading(false);
				}
			}

			input.addEventListener("input", function () {
				clearTimeout(timer);
				timer = setTimeout(function () {
					loadRows(input.value.trim());
				}, 250);
			});
		});
	}

	function boot() {
		initPageTransitions();
		initToasts();
		initAjaxAutocomplete();
		initAjaxTableSearches();
	}

	if (document.readyState === "loading") {
		document.addEventListener("DOMContentLoaded", boot);
	} else {
		boot();
	}
})();
