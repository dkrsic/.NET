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

	function initDateTimePickers() {
		const pickers = document.querySelectorAll('.app-datetime-picker');
		if (!pickers.length) return;

		const userLocale = navigator.language || document.documentElement.lang || 'hr';

		pickers.forEach(function (p) {
			const hidden = p.querySelector('input[type="hidden"]');
			const display = p.querySelector('input.display');
			const panel = p.querySelector('.picker-panel');

			function formatForDisplay(iso) {
				if (!iso) return '';
				const d = new Date(iso);
				if (isNaN(d)) return '';
				return d.toLocaleString(userLocale);
			}

			function setDisplay() {
				display.value = formatForDisplay(hidden.value);
			}

			setDisplay();

			p.querySelector('.toggle-picker').addEventListener('click', function () {
				if (!panel) return;
				panel.classList.toggle('d-none');
				panel.setAttribute('aria-hidden', panel.classList.contains('d-none'));

				if (!panel.classList.contains('d-none')) {
					// Custom calendar + time UI (avoid native browser datepicker)
					panel.innerHTML = '';

					const dateRow = document.createElement('div');
					dateRow.className = 'mb-2';
					const dateText = document.createElement('input');
					dateText.type = 'text';
					dateText.className = 'form-control mb-1';
					dateText.placeholder = 'YYYY-MM-DD';
					dateText.readOnly = true;

					const calendar = document.createElement('div');
					calendar.className = 'picker-calendar mb-2';

					const timeInput = document.createElement('input');
					timeInput.type = 'text';
					timeInput.className = 'form-control mb-2';
					timeInput.placeholder = 'HH:mm';

					dateRow.appendChild(dateText);
					panel.appendChild(dateRow);
					panel.appendChild(calendar);
					panel.appendChild(timeInput);

					// helper to render a simple month calendar
					function renderCalendar(year, month, selectedDay) {
						calendar.innerHTML = '';
						const header = document.createElement('div');
						header.className = 'd-flex justify-content-between align-items-center mb-1';
						const left = document.createElement('button'); left.type = 'button'; left.className = 'btn btn-outline-secondary btn-sm'; left.textContent = '<';
						const right = document.createElement('button'); right.type = 'button'; right.className = 'btn btn-outline-secondary btn-sm'; right.textContent = '>';
						const title = document.createElement('div'); title.textContent = new Date(year, month, 1).toLocaleString(userLocale, { month: 'long', year: 'numeric' });
						header.appendChild(left); header.appendChild(title); header.appendChild(right);
						calendar.appendChild(header);

						const grid = document.createElement('div');
						grid.style.display = 'grid';
						grid.style.gridTemplateColumns = 'repeat(7, 1fr)';
						grid.style.gap = '4px';

						const firstDay = new Date(year, month, 1).getDay();
						const daysInMonth = new Date(year, month + 1, 0).getDate();

						for (let i = 0; i < firstDay; i++) {
							const empty = document.createElement('div');
							grid.appendChild(empty);
						}

						for (let d = 1; d <= daysInMonth; d++) {
							const btn = document.createElement('button');
							btn.type = 'button';
							btn.className = 'btn btn-sm btn-light';
							btn.textContent = String(d);
							if (selectedDay === d) btn.classList.add('active');
							btn.addEventListener('click', function () {
								selectedDate = new Date(year, month, d);
								dateText.value = selectedDate.toISOString().slice(0, 10);
								// highlight selection
								Array.from(grid.querySelectorAll('button')).forEach(b => b.classList.remove('active'));
								btn.classList.add('active');
							});
							grid.appendChild(btn);
						}

						left.addEventListener('click', function () {
							const prev = new Date(year, month - 1, 1);
							renderCalendar(prev.getFullYear(), prev.getMonth(), selectedDate ? selectedDate.getDate() : null);
						});
						right.addEventListener('click', function () {
							const next = new Date(year, month + 1, 1);
							renderCalendar(next.getFullYear(), next.getMonth(), selectedDate ? selectedDate.getDate() : null);
						});

						calendar.appendChild(grid);
					}

					// initialize values
					let selectedDate = null;
					if (hidden.value) {
						const d = new Date(hidden.value);
						if (!isNaN(d)) {
							selectedDate = d;
							dateText.value = d.toISOString().slice(0, 10);
							timeInput.value = d.toISOString().slice(11, 16);
						}
					}
					const now = selectedDate || new Date();
					renderCalendar(now.getFullYear(), now.getMonth(), selectedDate ? selectedDate.getDate() : null);

					const actions = document.createElement('div');
					actions.className = 'd-flex gap-2 justify-content-end';
					const ok = document.createElement('button'); ok.type = 'button'; ok.className = 'btn btn-primary btn-sm'; ok.textContent = 'OK';
					const clear = document.createElement('button'); clear.type = 'button'; clear.className = 'btn btn-outline-secondary btn-sm'; clear.textContent = 'Obriši';
					actions.appendChild(clear); actions.appendChild(ok);
					panel.appendChild(actions);

					ok.addEventListener('click', function () {
						if (!dateText.value) {
							hidden.value = '';
							setDisplay();
							panel.classList.add('d-none');
							return;
						}
						// parse date and time (time optional)
						const parts = dateText.value.split('-').map(Number);
						const timeParts = (timeInput.value || '00:00').split(':').map(Number);
						const dt = new Date(parts[0], parts[1] - 1, parts[2], timeParts[0] || 0, timeParts[1] || 0);
						hidden.value = new Date(dt.getTime()).toISOString();
						setDisplay();
						panel.classList.add('d-none');
					});

					clear.addEventListener('click', function () {
						hidden.value = '';
						setDisplay();
						panel.classList.add('d-none');
					});
				}
			});
		});
	}

	function boot() {
		initPageTransitions();
		initToasts();
		initAjaxAutocomplete();
		initAjaxTableSearches();
		initDateTimePickers();
	}

	if (document.readyState === "loading") {
		document.addEventListener("DOMContentLoaded", boot);
	} else {
		boot();
	}
})();
