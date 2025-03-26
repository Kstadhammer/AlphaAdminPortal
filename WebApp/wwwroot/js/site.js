document.addEventListener("DOMContentLoaded", () => {
  const modalButtons = document.querySelectorAll('[data-modal="true"]');
  modalButtons.forEach((button) => {
    button.addEventListener("click", () => {
      const modalTarget = button.getAttribute("data-target");
      const modal = document.querySelector(modalTarget);
      if (modal) {
        modal.style.display = "flex";
      }
    });
  });

  // Add user dropdown functionality
  const settingsButton = document.getElementById("settingsButton");
  const userDropdown = document.getElementById("userDropdown");

  if (settingsButton && userDropdown) {
    settingsButton.addEventListener("click", (e) => {
      e.stopPropagation();
      userDropdown.style.display =
        userDropdown.style.display === "block" ? "none" : "block";
    });

    // Close dropdown when clicking outside
    document.addEventListener("click", (e) => {
      if (
        userDropdown.style.display === "block" &&
        !userDropdown.contains(e.target)
      ) {
        userDropdown.style.display = "none";
      }
    });

    // Dark mode toggle functionality
    const darkModeToggle = document.getElementById("darkModeToggle");
    if (darkModeToggle) {
      // Check for saved preference
      const darkMode = localStorage.getItem("darkMode") === "true";
      darkModeToggle.checked = darkMode;
      if (darkMode) document.body.classList.add("dark-theme");

      darkModeToggle.addEventListener("change", () => {
        if (darkModeToggle.checked) {
          document.body.classList.add("dark-theme");
          localStorage.setItem("darkMode", "true");
        } else {
          document.body.classList.remove("dark-theme");
          localStorage.setItem("darkMode", "false");
        }
      });
    }
  }
});

// Add event listeners for close buttons
const closeButtons = document.querySelectorAll('[data-close="true"]');
closeButtons.forEach((button) => {
  button.addEventListener("click", () => {
    // Find the closest parent modal
    const modal = button.closest(".modal");
    if (modal) {
      modal.style.display = "none";

      // Reset all forms in the modal
      modal.querySelectorAll("form").forEach((form) => {
        form.reset();

        // Reset image preview
        const profileLabel = form.querySelector(".profile-label");
        if (profileLabel) {
          // Restore the original camera icon content
          profileLabel.innerHTML = `
            <div class="camera-icon">
              <i class="fa-regular fa-camera"></i>
            </div>
          `;
        }
      });
    }
  });
  // Handle image preview
  document.querySelectorAll(".profile-input").forEach((input) => {
    input.addEventListener("change", async (event) => {
      const file = event.target.files[0];
      if (file) {
        const label = event.target.nextElementSibling;
        try {
          await processImage(file, label, label);
        } catch (error) {
          console.error("Failed to preview image:", error);
        }
      }
    });
  });
});

// Optional: Close modal when clicking outside the content
document.addEventListener("click", (event) => {
  if (event.target.classList.contains("modal")) {
    event.target.style.display = "none";
  }
});

// Add event listener for profile image upload (code from Hans Tips och Trix)

async function processImage(file, imagePreview, previewer, previewSize = 150) {
  try {
    const img = await loadImage(file);
    const canvas = document.createElement("canvas");
    canvas.width = previewSize;
    canvas.height = previewSize;

    const ctx = canvas.getContext("2d");
    ctx.drawImage(img, 0, 0, previewSize, previewSize);

    // Clear existing content
    imagePreview.innerHTML = "";

    // Create image element
    const previewImg = document.createElement("img");
    previewImg.src = canvas.toDataURL("image/jpeg");
    previewImg.style.width = "100%";
    previewImg.style.height = "100%";
    previewImg.style.borderRadius = "50%";
    previewImg.style.objectFit = "cover";

    // Add the image to the preview
    imagePreview.appendChild(previewImg);

    previewer.classList.add("selected");
  } catch (error) {
    console.error("Failed on image-processing:", error);
  }
}

function loadImage(file) {
  return new Promise((resolve, reject) => {
    const reader = new FileReader();
    reader.onload = (e) => {
      const img = new Image();
      img.onload = () => resolve(img);
      img.onerror = reject;
      img.src = e.target.result;
    };
    reader.onerror = reject;
    reader.readAsDataURL(file);
  });
}
